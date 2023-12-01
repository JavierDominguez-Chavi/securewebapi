using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using swebapi.Data;
using swebapi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace swebapi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{

    private readonly IdentityContext _context;
    private readonly IConfiguration _configuration;
    private readonly UserManager<CustomIdentityUser> _userManager;

    public LoginController(IdentityContext context, IConfiguration configuration, UserManager<CustomIdentityUser> userManager)
    {
        this._context = context;
        this._configuration = configuration;
        this._userManager = userManager;
    }

    // Get api/<CuentasController>
    [HttpGet]
    public IActionResult Get()
    {
        // Regresa 401 Acceso no Autorizado
        return Unauthorized("Acceso no autorizado!!! D:<");
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] LoginViewModel login)
    {
        // Verificamos credenciales con Identity
        var usuario = await _userManager.FindByEmailAsync(login.Correo);
        if (usuario is null || !await _userManager.CheckPasswordAsync(usuario, login.Password))
        {
            // Regresa 401 Acceso no Autorizado
            return Unauthorized("No autorizado!!!!!");

        }

        // Generacion de JWT

        // Generamos un token según los claims
        // Estos valores nos indicaran el usuario autenticado en cada peticion usando el token
        // Consu Id o Email es posible buscarlos
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, usuario.Id),
            new Claim(ClaimTypes.Name, usuario.UserName),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.GivenName, usuario.Nombre),
        };

        // Obtener los roles y agregarlos al claims
        var roles = await _userManager.GetRolesAsync(usuario);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // Creacion del token de acceso de 20min
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken
        (
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        // Regresa el token de acceso al usuario con validez de 20min
        return Ok(new
        {
            usuario.Id,
            usuario.Email,
            usuario.Nombre,
            rol = string.Join(",", roles),
            AccessToken = jwt,
        });
    }
}
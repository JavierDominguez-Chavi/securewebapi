using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using swebapi.Models;
using System.Runtime.CompilerServices;

namespace swebapi.Data.Seed
{
    public static class SeedIdentityUserData
    {
        public static void SeedUserIdentityData(this ModelBuilder modelBuilder)
        {
            //Agregar el rol "Administrador" a la tabla de AspNetRoles
            string AdministradorGeneralRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = AdministradorGeneralRoleId,
                Name = "Administrador",
                NormalizedName = "Administrador".ToUpper()
            });

            //Agregar el rol "Administrador" a la tabla de AspNetRoles
            string UsuarioGeneralRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = UsuarioGeneralRoleId,
                Name = "Usuario general",
                NormalizedName = "Usuario general".ToUpper()
            });
            //Agregar un usario a la tabla AspNetUsers
            var UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId, //Primary key
                    UserName = "josejdc070@gmail.com",
                    Email = "josejdc070@gmail.com",
                    NormalizedEmail = "josejdc070@gmail.com".ToUpper(),
                    Nombre = "José Javier Domínguez Carmona",
                    NormalizedUserName = "josejdc070@gmail.com".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "Diosesgrande*")
                }
             );

            //Aplicamos la relación entre el usuario y el rol en la tabla  AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = UsuarioId
                }
            );

            UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId,
                    UserName = "sperez@uv.mx",
                    Email = "sperez@uv.mx",
                    NormalizedEmail = "sperez@uv.mx".ToUpper(),
                    Nombre = "Saul Perez Garcia",
                    NormalizedUserName = "sperez@uv.mx".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "saulpwd")
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = UsuarioId
                }
            );

            UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId,
                    UserName = "gochoa@gmail.com",
                    Email = "gochoa@gmail.com",
                    NormalizedEmail = "gochoa@gmail.com".ToUpper(),
                    Nombre = "Gerardo Ochoa Martinez",
                    NormalizedUserName = "gochoa@gmail.com".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "saulpwd")
                }
            )
            ;

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = UsuarioId
                }
            );

        }
    }
}

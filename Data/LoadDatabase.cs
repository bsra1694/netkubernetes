using Microsoft.AspNetCore.Identity;
using NetKubernets.Models;

namespace NetKubernets.Data;

public class LoadDataBase{

    public static async Task InsertarData(AppDbContext context, UserManager<Usuario> usuarioManager){
        if(!usuarioManager.Users.Any())
        {
            var usuario = new Usuario{
                Nombre = "Santiago",
                Apellido = "Ruiz",
                Email = "bryrui.16@gmail.com",
                UserName = "santi.ruiz",
                Telefono = "0987924781"
            };

            await usuarioManager.CreateAsync(usuario, "PasswordSantiago1994*");
        }

        if(!context.Inmuebles!.Any())
        {
            context.Inmuebles!.AddRange(
                new Inmueble{
                    Nombre = "Casa de Playa",
                    Direccion = "Santa Elena",
                    Precio = 4500M,
                    FechaCreacion = DateTime.Now
                },
                new Inmueble{
                    Nombre = "Casa de Invierno",
                    Direccion = "Quito",
                    Precio = 3500M,
                    FechaCreacion = DateTime.Now
                }
            );
        }

        context.SaveChanges();

    }
}
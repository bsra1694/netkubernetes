using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernets.Middleware;
using NetKubernets.Models;
using NetKubernets.Token;

namespace NetKubernets.Data.Inmuebles;

public class InmuebleRepository : IImuebleRepository
{
    private readonly AppDbContext _contexto;
    public readonly IUsuarioSesion _usuarioSesion;
    private readonly UserManager<Usuario> _userManager; 

    public InmuebleRepository(
        AppDbContext contexto,
        IUsuarioSesion sesion,
        UserManager<Usuario> userManager)
    {
        _contexto = contexto;
        _usuarioSesion = sesion;
        _userManager = userManager;
    }
    public async Task CreateInmueble(Inmueble inmueble)
    {
        var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
        if(usuario is null)
        {
            throw new MiddlewareException(HttpStatusCode.Unauthorized,
            new {mensaje = "El usuario no es valido para hacer esta inserción"});
        }

        if(inmueble is null)
        {
            throw new MiddlewareException(HttpStatusCode.BadRequest,
            new {mensaje = "Los datos del inmueble son incorrctos"}); 
        }

        inmueble.FechaCreacion = DateTime.Now;
        inmueble.UsuarioId = Guid.Parse(usuario!.Id);


        await _contexto.Inmuebles!.AddAsync(inmueble);
    }

    public async Task DeleteInmueble(int id)
    {
        var inmueble = await _contexto.Inmuebles!.FirstOrDefaultAsync(x => x.Id == id);
        _contexto.Inmuebles!.Remove(inmueble!);
    }

    public async Task<IEnumerable<Inmueble>> GetAllInmuebles()
    {
        return await _contexto.Inmuebles!.ToListAsync();
    }

    public async Task<Inmueble> GetInmuebleById(int id)
    {
        var inmueble =await _contexto.Inmuebles!.FirstOrDefaultAsync(x => x.Id == id)!;
        if(inmueble is null)
        {
            throw new MiddlewareException(HttpStatusCode.NotFound,
            new {mensaje = "El inmueble no existe"});
        }else{
            return inmueble;
        }
    }

    public async Task<bool> SaveChanges()
    {
        return ( (await _contexto.SaveChangesAsync() >= 0) );
    }
}
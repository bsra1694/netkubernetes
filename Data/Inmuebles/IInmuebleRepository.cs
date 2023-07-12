using NetKubernets.Models;

namespace NetKubernets.Data.Inmuebles;

public interface IImuebleRepository{

    Task<bool> SaveChanges();

    Task<IEnumerable<Inmueble>> GetAllInmuebles();

    Task<Inmueble> GetInmuebleById(int id);

    Task CreateInmueble(Inmueble inmueble);

    Task DeleteInmueble(int id);

}
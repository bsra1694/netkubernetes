using NetKubernets.Models;

namespace NetKubernets.Token;

public interface IJwtGenerador{
    string CrearToken(Usuario usuario);
}
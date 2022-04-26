using Modelos;

namespace Datos.Interfaces
{
    public interface IRepositorioUsuarios
    {
        Task<Usuarios> GetPorCodigo(string codigo);
        Task<bool> ValidaUsuario(Login login);
    }
}

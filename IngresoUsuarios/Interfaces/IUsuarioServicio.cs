using Modelos;

namespace IngresoUsuarios.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<Usuarios> GetPorCodigo(string codigo);
    }
}

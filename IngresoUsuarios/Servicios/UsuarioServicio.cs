using Datos.Interfaces;
using Datos.Repositorios;
using IngresoUsuarios.Data;
using IngresoUsuarios.Interfaces;
using Modelos;
namespace IngresoUsuarios.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly MySQLConfiguration _configuration;
        private IRepositorioUsuarios repositorioUsuario;

        public UsuarioServicio(MySQLConfiguration configuration)
        {
            _configuration = configuration;
            repositorioUsuario = new RepositorioUsuario(configuration.CadenaConexion);
        }

        public async Task<Usuarios> GetPorCodigo(string codigo)
        {
            return await repositorioUsuario.GetPorCodigo(codigo);
        }
    }
}

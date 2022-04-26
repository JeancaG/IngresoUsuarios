using Datos.Interfaces;
using Datos.Repositorios;
using IngresoUsuarios.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;

namespace IngresoUsuarios.Controllers
{
    public class LoginController : Controller
    {
        private readonly MySQLConfiguration _configuration;
        private IRepositorioUsuarios _repositorioUsuario;

        public LoginController(MySQLConfiguration configuration)
        {
            _configuration = configuration;
            _repositorioUsuario = new RepositorioUsuario(configuration.CadenaConexion);
        }

        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(Login login)
        {
            string rol = string.Empty;
            try
            {
                bool usuarioValido = await _repositorioUsuario.ValidaUsuario(login);
                if (usuarioValido)
                {
                    Usuarios usu = await _repositorioUsuario.GetPorCodigo(login.Codigo);
                    if (usu.EstaActivo)
                    {
                        rol = usu.Rol;

                        //Añadimos los claims Usuario y Rol para tenerlos disponibles en la Cookie
                        var claims = new[]
                        {
                        new Claim(ClaimTypes.Name, usu.Codigo),
                        new Claim(ClaimTypes.Role, rol)
                    };

                        //Creamos el principal
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        //Generamos la cookie. SignInAsync es un método de extensión del contexto.
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                                                    new AuthenticationProperties
                                                    {
                                                        IsPersistent = true,
                                                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                                                    });
                    }
                    else
                    {
                        return LocalRedirect("/login/Usuario Inactivo");
                    }
                }
                else
                {
                    return LocalRedirect("/login/Datos de usuario Invalido");
                }
            }
            catch (Exception ex)
            {
                return LocalRedirect("/login/Datos de usuario Invalido");
            }
            return LocalRedirect("/");
        }

        [HttpGet("/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}


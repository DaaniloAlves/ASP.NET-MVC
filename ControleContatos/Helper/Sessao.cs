using ControleContatos.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControleContatos.Helper
{
    public class Sessao : ISessao
    {

        private readonly IHttpContextAccessor _contextAccessor;


        public Sessao(IHttpContextAccessor httpContext)
        {
            _contextAccessor = httpContext;
        }
        public UsuarioModel BuscarSessaoUsuario()
        {
           
                string sessaoUsuario = _contextAccessor.HttpContext.Session.GetString("sessaoUsuarioLogado");
            

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);
            return usuario;
        }

        public void CriarSessaoUsuario(UsuarioModel usuario)
        {
            string valor = JsonSerializer.Serialize(usuario);
            _contextAccessor.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoUsuario()
        {
            _contextAccessor.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}

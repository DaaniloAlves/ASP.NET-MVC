using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bc)
        {
            _bancoContext = bc;
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {                 
            usuario.DataCadastro = DateTime.Now;
            usuario.setSenhaHash();
            _bancoContext.Usuario.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public bool Apagar(int id)
        {   
            UsuarioModel usuario = ListarPorId(id);
            _bancoContext.Usuario.Remove(usuario);
            _bancoContext.SaveChanges();
            return true;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Login.ToLower() == login.ToLower());
        }

        public UsuarioModel BuscarPorLoginEEmail(string login, string email)
        {
            UsuarioModel usuarioEncontrado = _bancoContext.Usuario.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email.ToUpper() == email.ToUpper());
            return usuarioEncontrado;
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuario.ToList();
        }

        public UsuarioModel Editar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);
            usuarioDB.Nome  = usuario.Nome;
            usuarioDB.DataAtualizacao = DateTime.Now;
            usuarioDB.Senha = usuario.Senha;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.setSenhaHash();
            _bancoContext.Usuario.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuario.FirstOrDefault(x => x.Id == id);
        }
    }
}

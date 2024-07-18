using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Editar(UsuarioModel usuario);
        bool Apagar(int id);
        UsuarioModel ListarPorId(int id);

    }
}

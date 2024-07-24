using ControleContatos.Enums;
using ControleContatos.Helper;
using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuario")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o login do usuario")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o email do usuario")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insira o perfil do usuario")]
        public PerfilEnum? Perfil { get; set; }
        [Required(ErrorMessage = "Digite a senha do usuario")]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash() || Senha == senha; 

        }

        public void setSenhaHash()
        {
            Senha = Senha.GerarHash();
        }


        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha;
            return novaSenha;
        }
    }
}

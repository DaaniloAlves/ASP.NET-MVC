using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Insira um login")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Insira uma senha")]
        public string Senha { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Insira um login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Insira uma senha")]
        public string Email { get; set; }
    }
}

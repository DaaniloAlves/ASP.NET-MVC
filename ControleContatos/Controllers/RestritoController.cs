using ControleContatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaUsuarioLogado]

    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

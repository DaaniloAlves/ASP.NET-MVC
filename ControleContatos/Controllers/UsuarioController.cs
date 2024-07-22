using ControleContatos.Filters;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    [PaginaUsuarioAdm]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio up) {
            _usuarioRepositorio = up;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> listaUsuarios = _usuarioRepositorio.BuscarTodos();
            return View(listaUsuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            UsuarioModel teste = usuario;
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Criado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Deu erro, erro: {e}";
                return View();

            }

        }
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Editar(usuario);
                    TempData["MensagemSucesso"] = "Deu tudo certo!";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = "ModelState falso";
                return View(usuario);
            }
                
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Deu erro, erro: " + e.Message;
                return View();
            }
        }
    
    
    

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
        return View(usuario); 
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                    return RedirectToAction("Index");
                } else
                {
                    TempData["MensagemErro"] = "Deu erro";
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Deu erro, erro: " + e.Message;
                return RedirectToAction("Index");
            }
        }


    }
}

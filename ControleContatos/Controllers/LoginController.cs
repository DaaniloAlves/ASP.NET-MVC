using ControleContatos.Filters;
using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio u, ISessao sessao)
        {
            _usuarioRepositorio = u;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            if(_sessao.BuscarSessaoUsuario()  != null) // se logado, ja cai na home
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel login)     
        {
            try
            { 
                if (ModelState.IsValid)
                { 
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(login.Usuario);
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(login.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha invalida";
                    }
                    return RedirectToAction("Index", "Home");
                }
                TempData["MensagemErro"] = $"Usuario não encontrado, usuario invalido";
                return View("Index");
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Usuario não encontrado, erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult LinkRedefinirSenha()
        {
            return View();
        }


        [HttpPost]
        public IActionResult LinkRedefinirSenha(RedefinirSenhaModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioNovo = _usuarioRepositorio.BuscarPorLoginEEmail(usuario.Login, usuario.Email);
                    if (usuarioNovo != null)
                    {
                        usuarioNovo.GerarNovaSenha();
                        _usuarioRepositorio.Editar(usuarioNovo);
                        TempData["MensagemSucesso"] = "Login verificado com sucesso! Verifique seu email para visualizar a nova senha";
                        return RedirectToAction("Index", "Login");
                    }
                    return RedirectToAction("Index", "Login");
                }
                TempData["MensagemErro"] = $"Usuario não encontrado, usuario invalido";
                return View("Index");
            }

            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Usuario não encontrado, erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Deslogar()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

    }
}

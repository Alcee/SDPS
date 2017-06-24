using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using TCC_ALCINDO.Filters;
using TCC_ALCINDO.Models;

namespace TCC_ALCINDO.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly ClienteDAO _clienteDAO;
        public AccountController()
        {
            _clienteDAO = new ClienteDAO();
        }


        public ActionResult Visualizar()
        {

            Cliente cliente = _clienteDAO.Buscar(SessionUser.Id);

            return View(cliente);

        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");


            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = _clienteDAO.Autenticar(model);

                if (cliente != null)
                {
                    //Atribui o model a sessao
                    SessionUser = cliente;
                    //Seta o forme de autenticação
                    FormsAuthentication.SetAuthCookie(cliente.Email, false);

                    //Redireciona
                    return RedirectToAction("Visualizar");
                }
                else
                {
                    TempData["ErrorMsg"] = string.Format("Falha ao executar login.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Informações inválidas");
                return View(model);
            }
            
            
        }


        [AllowAnonymous]
        public ActionResult Cadastrar()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(Cliente model)
        {
            if(ModelState.IsValid)
            {

                _clienteDAO.Salvar(model);

                return RedirectToAction("Visualizar");
            }
            else
            {
                ModelState.AddModelError("", "Informações inválidas");
                return View(model);
            }
        }


        public ActionResult Editar(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Visualizar");

            Cliente cliente = _clienteDAO.Buscar(id);

            return View(cliente);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cliente model)
        {
           
            if (ModelState.IsValid)
            {
                _clienteDAO.Salvar(model);
                return RedirectToAction("Visualizar");
            }
            else
            {
                ModelState.AddModelError("", "Informações inválidas");
                return View(model);
            }
        }


        public ActionResult AlterarSenha()
        {
            return View();
        }


        public ActionResult AlterarSenha(LocalPasswordModel model)
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        
     
    }
}

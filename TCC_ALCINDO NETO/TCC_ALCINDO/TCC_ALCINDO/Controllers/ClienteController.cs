using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC_ALCINDO.Models;

namespace TCC_ALCINDO.Controllers
{
    public class ClienteController : BaseController
    {
        public ActionResult Index(string pesquisa)
        {
            try
            {
                var lst = new ClienteDAO().Listar(pesquisa);
                return View(lst);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = String.Format("Falha ao buscar clientes. {0}", ex.Message);
                return View(new List<Cliente>());
            }
        }
        public ActionResult Cadastro()
        {
            return View();
        }
        public ActionResult Editar(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }

            try
            {
                var obj = new ClienteDAO().Buscar(id);
                obj.Animais = new AnimalDAO().Listar(obj);
                return View(obj);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = String.Format("Não foi possível buscar o cliente. {0}", ex.Message);
                return View();
            }

        }
        public ActionResult Salvar(Cliente obj)
        {
            if (!obj.IsCpf())
                ModelState.AddModelError("CPF", "CPF inválido");

            if (!ModelState.IsValid)
                return View("Cadastro", obj);

            try
            {
                //var cliente = (Cliente)Session["cliente"];
                new ClienteDAO().Salvar(obj);//, cliente);
                TempData["SuccessMsg"] = "Cliente salvo com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = string.Format("Não foi possível buscar o cliente. {0}", ex.Message);

            }
            return RedirectToAction("Editar", new { @id = obj.Id });
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                //var usuario = (Usuario)Session["usuario"];
                new ClienteDAO().Delete(new Cliente { Id = id });//, usuario);
                TempData["SuccessMsg"] = "Cliente excluído com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = String.Format("Falha ao excluir cliente. {0}", ex.Message);
            }
            return RedirectToAction("Index");
        }

    }
}

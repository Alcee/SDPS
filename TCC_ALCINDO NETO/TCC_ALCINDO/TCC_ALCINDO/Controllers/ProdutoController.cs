using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC_ALCINDO.Models;

namespace TCC_ALCINDO.Controllers
{
    public class ProdutoController : BaseController
    {
        //
        // GET: /Produto/

        public ActionResult Index(string pesquisa)
        {
            try
            {
                var lst = new ProdutoDAO().Listar(pesquisa);
                return View(lst);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = String.Format("Falha ao buscar clientes. {0}", ex.Message);
                return View(new List<Produto>());
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
                var obj = new ProdutoDAO().Buscar(id);
                return View(obj);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = String.Format("Não foi possível buscar o item. {0}", ex.Message);
                return View();
            }

        }

        public ActionResult Salvar(Produto obj)
        {

            if (!ModelState.IsValid)
                return View("Cadastro", obj);

            try
            {
                //var usuario = (Usuario)Session["usuario"];
                new ProdutoDAO().Salvar(obj);//, usuario);
                TempData["SuccessMsg"] = "Item salvo com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = string.Format("Não foi possível buscar o item. {0}", ex.Message);

            }
            return View("Editar", obj);
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                //var usuario = (Usuario)Session["usuario"];
                new ProdutoDAO().Delete(new Produto { Id = id });//, usuario);
                TempData["SuccessMsg"] = "Item excluído com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = String.Format("Falha ao excluir item. {0}", ex.Message);
            }
            return RedirectToAction("Index");
        }

    }
}

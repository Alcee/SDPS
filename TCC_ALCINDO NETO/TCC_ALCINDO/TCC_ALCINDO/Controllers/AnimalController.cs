using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TCC_ALCINDO.Models;


namespace TCC_ALCINDO.Controllers
{
    [Authorize]
    public class AnimalController : BaseController
    {
        private readonly AnimalDAO _animalDAL;

        public AnimalController()
        {
            _animalDAL = new AnimalDAO();
        }

        public ActionResult Index(string pesquisa)
        {
            List<Animal> list = _animalDAL.Listar(pesquisa);

            return View(list);
        }

        public ActionResult Detalhes(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            Animal animal = _animalDAL.Buscar(id);

            return View(_animalDAL.Buscar(id));
        }

        public ActionResult Cadastro()
        {
            var animal = new Animal(SessionUser.Id);

            return View(animal);
        }

        public ActionResult Editar(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }

            try
            {
                var obj = _animalDAL.Buscar(id);
                return View(obj);
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMsg"] = ex.Message;
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = String.Format("Não foi possível buscar o animal. {0}", ex.Message);
                return View();
            }
        }


        public ActionResult Salvar(Animal obj,int idCliente, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                
                if (upload != null && upload.ContentLength > 0)
                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        obj.Foto = reader.ReadBytes(upload.ContentLength);
                    }

                try
                {
                    obj.Cliente = new Cliente()
                    {
                        Id = idCliente
                    };

                    _animalDAL.Salvar(obj);
                    TempData["SuccessMsg"] = "Animal salvo com sucesso!";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMsg"] = string.Format("Falha ao salvar animal. {0}", ex.Message);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(obj.Id == 0 ? "Cadastro" : "Editar", obj);
            }
        }


        public ActionResult Excluir(int id)
        {
            try
            {
                //var usuario = (Usuario)Session["usuario"];
                _animalDAL.Delete(new Animal(SessionUser.Id) { Id = id });//, usuario);
                TempData["SuccessMsg"] = "Animal excluído com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = String.Format("Falha ao excluir animal. {0}", ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult VisualizarAnimal(int id)
        {
            Animal animal = _animalDAL.Buscar(id);

            return File(animal.Foto, "image/jpg");
        }
    }
}

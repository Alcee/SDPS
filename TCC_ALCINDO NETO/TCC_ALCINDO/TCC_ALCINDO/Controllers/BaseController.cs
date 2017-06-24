using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCC_ALCINDO.Models;

namespace TCC_ALCINDO.Controllers
{
    public class BaseController : Controller
    {

        public Cliente SessionUser
        {

            get
            {
                Cliente cliente  = (Cliente)Session["Usuario"] ?? new Cliente();

                if(cliente == null)
                {
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    Session.Clear();
                }
                return cliente;
            }
            set
            {
                if (Session["Usuario"] == null)
                {
                    Session["Usuario"] = value;
                    Session.Timeout = 30;
                }

            }


        }

    }
}
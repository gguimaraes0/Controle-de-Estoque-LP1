using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeEstoque.Controllers
{
    public class DefaultController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HelperController.VerificaUserLogado(HttpContext.Session))
            {
                ViewBag.Logado = false;
                context.Result = View("../Home/Index");
            }
            else
            {
                ViewBag.Logado = true;
                ViewBag.nome_usuario = HelperController.getUserName(HttpContext.Session);
                ViewBag.id_usuario = HelperController.getId(HttpContext.Session);
                base.OnActionExecuting(context);
            }
        }
    }
}

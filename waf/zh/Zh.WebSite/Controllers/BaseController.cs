using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Zh.WebSite.Controllers
{
	public class BaseController : Controller
    {

	    /// <summary>
	    /// Egy akció meghívása után végrehajtandó metódus.
	    /// </summary>
	    /// <param name="context">Az akció kontextus argumentuma.</param>
	    public override void OnActionExecuted(ActionExecutedContext context)
	    {
		    base.OnActionExecuted(context);

		    ViewBag.CurrentUserName = String.IsNullOrEmpty(User.Identity.Name) ? null : User.Identity.Name;
	    }
	}
}
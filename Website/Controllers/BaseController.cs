using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Website.Models;


namespace Website.Controllers
{
    public class BaseController : Controller
    {
		protected readonly ISysService sysService;

        public BaseController( ISysService sysService )
        {
            this.sysService = sysService;
        }

		public override void OnActionExecuted( ActionExecutedContext context )
		{
			base.OnActionExecuted( context );

			ViewBag.CurUserName = string.IsNullOrEmpty( User.Identity.Name ) ? null : User.Identity.Name;
		}
	}
}
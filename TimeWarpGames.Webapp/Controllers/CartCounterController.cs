using System.Web;
using System.Web.Mvc;
using TimeWarpGames.Bll;

public class BaseController : Controller
{
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var session = new HttpSessionStateWrapper(System.Web.HttpContext.Current.Session);
        var cartBll = new ShoppingCartBll(session);
        ViewBag.CartCount = cartBll.GetItemCount();

        base.OnActionExecuting(filterContext);
    }
}
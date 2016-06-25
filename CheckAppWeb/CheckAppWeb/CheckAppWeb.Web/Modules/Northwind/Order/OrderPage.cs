﻿

//[assembly:Serenity.Navigation.NavigationLink(int.MaxValue, "Northwind/Order", url: "~/Northwind/Order", permission: "Northwind")]

namespace CheckAppWeb.Northwind.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Northwind/Order"), Route("{action=index}")]
    public class OrderController : Controller
    {
        [PageAuthorize(Northwind.PermissionKeys.General)]
        public ActionResult Index()
        {
            return View(MVC.Views.Northwind.Order.OrderIndex);
        }
    }
}
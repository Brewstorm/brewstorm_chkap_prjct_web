﻿
namespace CheckAppWeb.AdminLTE
{
    using System.Web.Mvc;

    [Authorize, RoutePrefix("AdminLTE/Tables"), Route("{action=index}")]
    public class TablesController : Controller
    {
        public ActionResult SimpleTables()
        {
            return View(MVC.Views.AdminLTE.Tables.SimpleTables);
        }

        public ActionResult DataTables()
        {
            return View(MVC.Views.AdminLTE.Tables.DataTables);
        }
    }
}
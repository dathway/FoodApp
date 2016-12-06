using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_FoodCalc.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        public ContentResult RenderJson(string input)
        {
            var ct = new ContentResult();
            ct.ContentEncoding = System.Text.Encoding.Default;
            ct.ContentType = "application/json";
            ct.Content = input;
            return ct;
        }

    }
}

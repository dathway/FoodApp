using System;
using ActionService;
using AutoMapper;
using BusinessObjects;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_FoodCalc.Models;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace MVC_FoodCalc.Controllers
{
    public class HomeController : Controller
    {
        IService service { get; set; }

        // static constructor. establishes Automapper object maps
        static HomeController()
        {
            Mapper.CreateMap<Ingredient, IngredientModel>();
        }        

        //default constructor        
        public HomeController() : this(new Service()) { }
        
        // overloaded 'injectable' constructor
        // ** Constructor Dependency Injection (DI).
        public HomeController(IService service) { this.service = service; }

        public ActionResult Index()
        {
            ViewBag.Title = "Nutrition Calculator";
            ViewBag.Message = "Search for foods and find their nutrition values.";

            return View();
        }

        public ActionResult Food(int id)
        {                 
            return View("/Views/Home/ngFood.cshtml");
        }

        [HttpGet]
        public ContentResult GetFood(int foodId, int serving = 1, string measurement = "0")
        {
            IngredientModel model = null;

            var food = service.CalculateFood(foodId, measurement, serving);
            model = Mapper.Map<Ingredient, IngredientModel>(food);

            List<SelectListItem> measurements = new List<SelectListItem>();
            measurements.Add(new SelectListItem() { Text = "100 grams", Value = "0", Selected = (measurement == "0") });
            measurements.Add(new SelectListItem() { Text = model.GmWt_Desc1, Value = "1", Selected = (measurement == "1") });
            if (model.GmWt_Desc2 != null)
            {
                measurements.Add(new SelectListItem() { Text = model.GmWt_Desc2, Value = "2", Selected = (measurement == "2") });
            }
            model.Measurement = measurements;
            var result = JsonConvert.SerializeObject(model);
            Response.AddHeader("Expires", "-1"); //consider adding this to a base controller.
            return Content(result, "application/json");
        }

        public ActionResult Foodtoo(int foodId, int serving = 1, string measurement = "0")
        {
            IngredientModel model = null;

            var food = service.CalculateFood(foodId, measurement, serving);
            model = Mapper.Map<Ingredient, IngredientModel>(food);

            List<SelectListItem> measurements = new List<SelectListItem>();
            measurements.Add(new SelectListItem() { Text = "100 grams", Value = "0", Selected = (measurement == "0") });
            measurements.Add(new SelectListItem() { Text = model.GmWt_Desc1, Value = "1", Selected = (measurement == "1") });
            if (model.GmWt_Desc2 != null)
            {
                measurements.Add(new SelectListItem() { Text = model.GmWt_Desc2, Value = "2", Selected = (measurement == "2") });
            }
            model.Measurement = measurements;
            //var result = JsonConvert.SerializeObject(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Food(FormCollection collection)
        {
            IngredientModel model = null;
           
            var foodId = int.Parse(collection["foodId"]);
            var measurement = (collection["measurements"] != null) ? collection["measurements"] : "0";            
            var serving = (collection["Serving"] != null) ? int.Parse(collection["Serving"]) : 1;

            var food = service.CalculateFood(foodId, measurement, serving);
            model = Mapper.Map<Ingredient, IngredientModel>(food);

            List<SelectListItem> measurements = new List<SelectListItem>();
            measurements.Add(new SelectListItem() { Text = "100 grams", Value = "0", Selected = (measurement == "0") });
            measurements.Add(new SelectListItem() { Text = model.GmWt_Desc1, Value = "1", Selected = (measurement == "1") });
            if (model.GmWt_Desc2 != null)
            {
                measurements.Add(new SelectListItem() { Text = model.GmWt_Desc2, Value = "2", Selected = (measurement == "2") });
            }
            model.Measurement = measurements;
            return View(model);
        }

        public ActionResult Search(string FoodSearch)
        {
            var model = new IngredientsModel();
            var results = service.SearchIngredients(FoodSearch, 100);
            model.Ingredients = Mapper.Map<List<Ingredient>, List<IngredientModel>>(results);
            return View(model);
        }

        public JsonResult Autocomplete(string term)
        {
            var model = new IngredientsModel();
            var items = service.SearchIngredients(term, 10);
            var results = (from i in items
                           select new { id = i.NDB_No, value= i.Shrt_Desc}
                               );                  
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

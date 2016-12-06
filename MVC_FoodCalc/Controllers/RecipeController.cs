using System;
using ActionService;
using AutoMapper;
using BusinessObjects;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_FoodCalc.Models;
using MVC_FoodCalc.ViewModels;

namespace MVC_FoodCalc.Controllers
{
    public class RecipeController : BaseController
    {
        IService service { get; set; }

        // static constructor. establishes Automapper object maps
        //static RecipeController()
        //{
        //    Mapper.CreateMap<Recipe, IngredientModel>();
        //} 
        //default constructor        
        public RecipeController() : this(new Service()) { }
        // overloaded 'injectable' constructor
        // ** Constructor Dependency Injection (DI).
        public RecipeController(IService service) { this.service = service; }


        public ActionResult Index()
        {
            var model = service.GetAllRecipes();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult Create(FormCollection collection)
        {
            var recipe = new Recipe()
            {
                CookTime_m = int.Parse(collection["CookTime_m"]),
                Directions = collection["Directions"],
                ImgUrl = collection["ImgUrl"],
                Name = collection["Name"],
                PrepTime_m = int.Parse(collection["PrepTime_m"]),
                Rating = Decimal.Parse(collection["Rating"]),
                ReadyTime_m = int.Parse(collection["ReadyTime_m"])
            };

            service.AddRecipe(recipe);
            return Redirect("index");
        }

        public ActionResult Details(int Id)
        {
            RecipeViewModel vm = new RecipeViewModel();
            var recipe = service.GetRecipe(Id);
            vm.recipe = recipe;
            vm.calculation = service.CalculateRecipe(recipe);
            return View(vm);
        }

        public ActionResult Edit(int Id)
        {
            var model = service.GetRecipe(Id);
            return View(model);
        }

        [HttpPost]
        public RedirectResult Edit(int Id, FormCollection collection)
        {
            var recipe = service.GetRecipe(Id);
           recipe.CookTime_m = int.Parse(collection["CookTime_m"]);
           recipe.Directions = collection["Directions"];
           recipe.ImgUrl = collection["ImgUrl"];
           recipe.Name = collection["Name"];
           recipe.PrepTime_m = int.Parse(collection["PrepTime_m"]);
           recipe.Rating = Decimal.Parse(collection["Rating"]);
           recipe.ReadyTime_m = int.Parse(collection["ReadyTime_m"]);
          
            service.ModifyRecipe(recipe);
            return Redirect("~/Recipe/Details/" + Id);
        }

        [HttpPost]
        public ContentResult RemoveIngredient(int lkupId)
        {
            var success_output = false;
            try
            {
                success_output = service.RemoveRecipeDetails(lkupId);
            }
            catch(Exception e)
            {
                success_output = false;
            }

            var result = new { success = success_output };
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string output = serializer.Serialize(result);
            return RenderJson(output);
        }

    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects
{
    public interface IRecipeDao
    {
        List<Recipe> GetAllRecipes();
        Recipe GetRecipe(int recipeId);
        void AddRecipe(Recipe recipe);
        void ModifyRecipe(Recipe recipe);
    }

    public class RecipeDao : IRecipeDao
    {
        static RecipeDao()
        {
            Mapper.CreateMap<RecipeEntity, Recipe>();
            Mapper.CreateMap<Recipe, RecipeEntity>();
        }
    
        public Recipe GetRecipe(int recipeId)
        {
            using (var context = new NutritionEntities())
            {
                var result = context.RecipeEntities.Include("RecipeDetailsEntities").SingleOrDefault(r => r.Id == recipeId);
                return Mapper.Map<RecipeEntity, Recipe>(result);
            }            
        }
        public List<Recipe> GetAllRecipes()
        {
            using (var context = new NutritionEntities())
            {
                var results = context.RecipeEntities.ToList();
                return Mapper.Map<List<RecipeEntity>, List<Recipe>>(results);
            }
        }

        public void AddRecipe(Recipe recipe)
        {
            using (var context = new NutritionEntities())
            {
                var recipeEntity = Mapper.Map<Recipe, RecipeEntity>(recipe);
                context.RecipeEntities.Add(recipeEntity);
                context.SaveChanges();
            }
        }
        public void ModifyRecipe(Recipe recipe)
        {
            using (var context = new NutritionEntities())
            {
                var updateEntry = context.RecipeEntities.Include("RecipeDetailsEntity").Where(x => x.Id == recipe.Id).FirstOrDefault();
                updateEntry.Directions = recipe.Directions;
                updateEntry.ImgUrl = recipe.ImgUrl;
                updateEntry.Name = recipe.Name;
                updateEntry.PrepTime_m = recipe.PrepTime_m;
                updateEntry.Rating = recipe.Rating;
                updateEntry.ReadyTime_m = recipe.ReadyTime_m;
                
                var updateRecipeDetails = Mapper.Map<List<RecipeDetails>, List<RecipeDetailsEntities>>(recipe.RecipeDetailsEntities);
                updateEntry.RecipeDetailsEntities = updateRecipeDetails;

                //Potential code to use to modify the Recipe details if the above does not work.
                ////remove deleted details
                //order.Details
                //.Where(d => !orderDTO.Details.Any(detailDTO => detailDTO.Id == d.Id))
                //.Each(deleted => ctx.DetailSet.Remove(deleted));

                ////update or add details
                //orderDTO.Details.Each(detailDTO =>
                //{
                //    var detail = order.Details.FirstOrDefault(d => d.Id == detailDTO.Id);
                //    if (detail == null)
                //    {
                //        detail = new Detail();
                //        order.Details.Add(detail);
                //    }
                //    detail.Name = detailDTO.Name;
                //    detail.Quantity = detailDTO.Quantity;
                //});

                context.SaveChanges();
            }
        }        


        

    }
}

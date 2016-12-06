using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataObjects
{
    public interface IRecipeDetailsDao
    {
        List<RecipeDetails> GetRecipeDetails(int recipeId);
        void AddRecipeDetail(Recipe recipe, Ingredient ingredient, int serving, int measurement);
        bool RemoveRecipeDetail(int LkupId);
    }

    public class RecipeDetailsDao: IRecipeDetailsDao
    {
        static RecipeDetailsDao()
        {
            Mapper.CreateMap<RecipeDetailsEntities, RecipeDetails>();
        }

        public List<RecipeDetails> GetRecipeDetails(int recipeId)
        {
            using (var context = new NutritionEntities())
            {
                var recipeDetails = context.RecipeDetailsEntities.Where(x => x.Recipe_Id == recipeId);
                var results = Mapper.Map<IEnumerable<RecipeDetailsEntities>, List<RecipeDetails>>(recipeDetails);
                return results;
            }
        }

        public RecipeDetailsEntities GetRecipeDetail(int lkupId)
        {
            using (var context = new NutritionEntities())
            {
                return context.RecipeDetailsEntities.Where(x => x.RecipeLkup_Id == lkupId).FirstOrDefault();                
            }
        }

        public void AddRecipeDetail(Recipe recipe, Ingredient ingredient, int serving, int measurement)
        {
            using (var context = new NutritionEntities())
            {
                var recipeDetails = new RecipeDetails()
                {
                    Ingredient_Id = ingredient.NDB_No,
                    Measurement = measurement,
                    Serving = serving,
                    Recipe_Id = recipe.Id
                };
                var entity = Mapper.Map<RecipeDetails, RecipeDetailsEntities>(recipeDetails);
                context.RecipeDetailsEntities.Add(entity);
                
            }
        }

        public bool RemoveRecipeDetail(int LkupId)
        {
            var success = false;
            using (var context = new NutritionEntities())
            {
                try
                {
                    var recipeDetail = GetRecipeDetail(LkupId);
                    context.RecipeDetailsEntities.Remove(recipeDetail);
                    context.SaveChanges();
                }
                catch(Exception e)
                {
                    success = false;
                }
            }
            return success;            
        }

    }
}

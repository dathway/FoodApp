using System;
using BusinessObjects;
using DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionService
{
    public interface IService
    {
        // Ingredients
        Ingredient GetIngredient(int foodId);
        List<Ingredient> SearchIngredients(string search, int numResults);
        Ingredient CalculateFood(int foodId, string measurement, int serving);
        

        // Recipes
        Recipe GetRecipe(int recipeId);
        List<Recipe> GetAllRecipes();
        void AddRecipe(Recipe recipe);
        void ModifyRecipe(Recipe recipe);
        RecipeCalculation CalculateRecipe(Recipe recipe);

        // Recipe Details
        List<RecipeDetails> GetRecipeDetails(int Recipe_Id);
        bool RemoveRecipeDetails(int LkupId);

    }

    public class Service: IService
    {
        static readonly IIngredientDao foodDao = new IngredientDao();
        static readonly IRecipeDao recipeDao = new RecipeDao();
        static readonly IRecipeDetailsDao recipeDetailsDao = new RecipeDetailsDao();

        // Ingredients
        public Ingredient GetIngredient(int foodId)
        {
            var food = foodDao.GetIngredient(foodId);
            return food;
        }
        public List<Ingredient> SearchIngredients(string search, int numResults)
        {
            List<Ingredient> results = foodDao.SearchIngredients(search, numResults);
            return results;
        }
        public Ingredient CalculateFood(int foodId, string measurement, int serving)
        {
            Ingredient food = GetIngredient(foodId);
            Ingredient tempFood = new Ingredient();

            decimal ratioModifier = GetMeasurementRatioModifier(food, measurement);

            tempFood.NDB_No = food.NDB_No;           
            tempFood.Shrt_Desc = food.Shrt_Desc;
            tempFood.Energ_Kcal = (int)Math.Round((food.Energ_Kcal * serving) * ratioModifier);
            tempFood.Carbohydrt_g = (food.Carbohydrt_g * serving) * ratioModifier;
            tempFood.Cholestrl_mg = (int)Math.Round((food.Cholestrl_mg * serving) * ratioModifier);
            tempFood.FA_Mono_g = (food.FA_Mono_g * serving) * ratioModifier;
            tempFood.FA_Poly_g = (food.FA_Poly_g * serving) * ratioModifier;
            tempFood.FA_Sat_g = (food.FA_Sat_g * serving) * ratioModifier;
            tempFood.Fiber_TD_g = (food.Fiber_TD_g * serving) * ratioModifier;
            tempFood.Lipid_Tot_g = (food.Lipid_Tot_g * serving) * ratioModifier;
            tempFood.Protein_g = (food.Protein_g * serving) * ratioModifier;
            tempFood.Sugar_Tot_g = (food.Sugar_Tot_g * serving) * ratioModifier;
            tempFood.Sodium_mg = (int)Math.Round((food.Sodium_mg * serving) * ratioModifier);
            tempFood.Potassium_mg = (int)Math.Round((food.Potassium_mg * serving) * ratioModifier);
            tempFood.GmWt_1 = food.GmWt_1 * serving;
            tempFood.GmWt_Desc1 = food.GmWt_Desc1;
            tempFood.GmWt_2 = food.GmWt_2 * serving;
            tempFood.GmWt_Desc2 = food.GmWt_Desc2;
            tempFood.Serving = serving;

            food = tempFood;
            return food;
        }

        // Recipes
        public Recipe GetRecipe(int recipeId)
        {
            return recipeDao.GetRecipe(recipeId);
        }
        public List<Recipe> GetAllRecipes()
        {
            return recipeDao.GetAllRecipes();
        }
        public void AddRecipe(Recipe recipe)
        {
            recipeDao.AddRecipe(recipe);
        }
        public void ModifyRecipe(Recipe recipe)
        {
            recipeDao.ModifyRecipe(recipe);
        }


        // Recipe Details
        public List<RecipeDetails> GetRecipeDetails(int Recipe_Id)
        {
            return recipeDetailsDao.GetRecipeDetails(Recipe_Id);
        }
        public bool RemoveRecipeDetails(int LkupId)
        {
            return recipeDetailsDao.RemoveRecipeDetail(LkupId);            
        }

        // Measurement Calculations
        private decimal GetMeasurementRatioModifier(Ingredient food, string measurement)
        {
            decimal result;
            switch (measurement)
            {
                case "0": result = (decimal)1.0;
                    break;
                case "1": result = (decimal)food.GmWt_1 / 100;
                    break;
                case "2": result = (decimal)food.GmWt_2 / 100;
                    break;
                default: result = (decimal)1.0;
                    break;
            }            
            return result;            
        }

        public RecipeCalculation CalculateRecipe(Recipe recipe)
        {
            RecipeCalculation result = new RecipeCalculation();
            foreach (var ing in recipe.RecipeDetailsEntities)
            {
                Ingredient ingredient = CalculateFood(ing.Ingredient_Id, ing.Measurement.ToString(), ing.Serving);
                result.Carbohydrt_g += ingredient.Carbohydrt_g;
                result.Cholestrl_mg += ingredient.Cholestrl_mg;
                result.Energ_Kcal += ingredient.Energ_Kcal;
                result.FA_Mono_g += ingredient.FA_Mono_g;
                result.FA_Poly_g += ingredient.FA_Poly_g;
                result.FA_Sat_g += ingredient.FA_Sat_g;
                result.Fiber_TD_g += ingredient.Fiber_TD_g;
                result.Lipid_Tot_g += ingredient.Lipid_Tot_g;
                result.Potassium_mg += ingredient.Potassium_mg;
                result.Protein_g += ingredient.Protein_g;
                result.Sodium_mg += ingredient.Sodium_mg;
                result.Sugar_Tot_g += ingredient.Sugar_Tot_g;                
            }
            return result;
        }

    }
}

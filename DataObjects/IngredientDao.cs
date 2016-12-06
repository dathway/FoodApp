using AutoMapper;
using System;
using BusinessObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Objects.SqlClient;
using System.Collections;

namespace DataObjects
{
    public interface IIngredientDao
    {
        List<Ingredient> SearchIngredients(string search, int numResults);
        Ingredient GetIngredient(int foodId);        
    }

    public class IngredientDao : IIngredientDao
    {
        static IngredientDao()
        {
            Mapper.CreateMap<IngredientEntity, Ingredient>();            
        }

        public Ingredient GetIngredient(int foodId)
        {
            using (var context = new NutritionEntities())
            {
                var food = context.IngredientEntities.SingleOrDefault(f => f.NDB_No == foodId);
                return Mapper.Map<IngredientEntity, Ingredient>(food);
            }            
        }

        public List<Ingredient> SearchIngredients(string search, int numResults=50)
        {
            using (var context = new NutritionEntities())
            {
                //Here wer are using patindex to order our results by the position of the search string.
                var results = context.IngredientEntities.Where(r => r.Shrt_Desc.Contains(search)).OrderBy(x => SqlFunctions.PatIndex("%" + search + "%", x.Shrt_Desc)).Take(numResults).ToList();  
                return Mapper.Map<List<IngredientEntity>, List<Ingredient>>(results);
            }
        }

    }
}

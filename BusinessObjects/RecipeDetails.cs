using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class RecipeDetails
    {
        public int RecipeLkup_Id { get; set; }
        public int Recipe_Id { get; set; }
        public int Ingredient_Id { get; set; }        
        public int Serving { get; set; }
        public int Measurement { get; set; }

        public virtual Ingredient IngredientEntity { get; set; }
        public virtual Recipe RecipeEntity { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PrepTime_m { get; set; }
        public int CookTime_m { get; set; }
        public int ReadyTime_m { get; set; }
        public string ImgUrl { get; set; }
        public string Directions { get; set; }
        public decimal Rating { get; set; }

        public List<RecipeDetails> RecipeDetailsEntities { get; set; }

    }
}

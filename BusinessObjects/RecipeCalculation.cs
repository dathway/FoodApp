using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class RecipeCalculation
    {
        public int NDB_No { get; set; }
        public int Energ_Kcal { get; set; }
        public string Shrt_Desc { get; set; }
        public decimal Protein_g { get; set; }
        public decimal Lipid_Tot_g { get; set; }
        public decimal Carbohydrt_g { get; set; }
        public decimal Fiber_TD_g { get; set; }
        public decimal Sugar_Tot_g { get; set; }
        public decimal FA_Sat_g { get; set; }
        public decimal FA_Mono_g { get; set; }
        public decimal FA_Poly_g { get; set; }
        public int Sodium_mg { get; set; }
        public int Potassium_mg { get; set; }
        public int Cholestrl_mg { get; set; }

    }
}

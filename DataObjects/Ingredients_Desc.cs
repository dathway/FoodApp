//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataObjects
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingredients_Desc
    {
        public int NDB_No { get; set; }
        public string FdGrp_Cd { get; set; }
        public string Long_Desc { get; set; }
        public string Shrt_Desc { get; set; }
        public string ComName { get; set; }
        public string ManufacName { get; set; }
        public string Survey { get; set; }
        public string Ref_Desc { get; set; }
        public Nullable<short> Refuse { get; set; }
        public string SciName { get; set; }
        public Nullable<float> N_Factor { get; set; }
        public Nullable<float> Pro_Factor { get; set; }
        public Nullable<float> Fat_Factor { get; set; }
        public Nullable<float> CHO_Factor { get; set; }
    
        public virtual IngredientEntity IngredientEntities { get; set; }
    }
}

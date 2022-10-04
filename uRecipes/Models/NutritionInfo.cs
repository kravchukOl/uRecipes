using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uRecipes.Models
{
    [Table("nutrition")]
    public class NutritionInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public int Energy_kJ { get; set; }
        public int Calories_kcal { get; set; }
        public int Fat_g { get; set; }
        public int Carbohydrate_g { get; set; }
        public int Sugar_g { get; set; }
        public int Fiber_g { get; set; }
        public int Protein_g { get; set; }
        public int Cholesterol_mg { get; set; }
        public int Sodium_mg { get; set; }
    }
}

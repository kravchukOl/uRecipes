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
        public string Calories { get; set; }
        public string Fat { get; set; }
        public string Carbs { get; set; }
        public string Fiber { get; set; }
        public string Sugar { get; set; }
        public string Protein { get; set; }
    }
}

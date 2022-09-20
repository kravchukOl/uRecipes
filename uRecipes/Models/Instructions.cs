using SQLite;

namespace uRecipes.Models
{
    [Table("instruction")]
    public class Instructions
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string InstructionsJSON { get; set; }

        /* !!! Not implemented !!! */
        /* !!! Not defined !!! */
    }
}

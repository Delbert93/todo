using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diabetes.Model
{
    [Table("Ingedient")]
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string  Name { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(typeof(Recipe))]
        public int RecipeID { get; set; }
        public override string ToString()
        {
            return $"{ID}: {Name} ({Quantity}) from recipe {RecipeID}";
        }
    }
}

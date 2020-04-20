using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diabetes.Model
{
    [Table("Recipe")]
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Unique]
        public string Name { get; set; }

        public string Description { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]

        public List<Ingredient> Ingredients { get; set; }

        //List of steps 2.0

        //public int CategoryId { get; set; } 2.0
    }
}

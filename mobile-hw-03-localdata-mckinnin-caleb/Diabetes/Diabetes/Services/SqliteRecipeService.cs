using Diabetes.Model;
using Diabetes.Services.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Services
{
    //Ingredient submitIngredient
    public class SqliteRecipeService : IRecipeService
    {
        public async Task<Recipe> SubmitRecipe(Recipe submitRecipe)
        {
            throw new Exception();
        }

        public async Task<Recipe> AddIngredient(Recipe addRecipe)
        {
            throw new Exception();
        }

        public async Task<Recipe> SaveRecipe(Recipe submitRecipe)
        {
            throw new Exception();
        }


        //this is what sends out to the database


    }

    
}

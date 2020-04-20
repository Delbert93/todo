using Diabetes.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diabetes.Services.Data
{
    public interface IRecipeService
    {
        Task<Recipe> SubmitRecipe(Recipe submitRecipe);
        Task<Recipe> AddIngredient(Recipe addIngredient);
        //, Ingredient submitIngredient
        Task<Recipe> SaveRecipe(Recipe submitRecipe);
    
    }
}

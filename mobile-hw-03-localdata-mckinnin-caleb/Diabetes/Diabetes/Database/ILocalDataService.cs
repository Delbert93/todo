using Diabetes.Model;
using System.Collections.Generic;
using System.Text;

namespace Diabetes.Database
{
    public interface ILocalDataService
    {
        void Initialize();
        void CreateRecipe(Recipe recipe);

        void UpdateRecipe(Recipe recipe);
        IEnumerable<Recipe> GetRecipes();
        IEnumerable<Ingredient> GetIngredients();
        void DeleteRecipe(Recipe recipe);
        void CreateIngredient(Ingredient ingredient);

        void DeleteIngredient(Ingredient ingredient);

        void AddItem(Recipe recipe);
        void UpdateItem(Recipe recipe);
    }
}

using Diabetes.Model;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Diabetes.Database
{
    public class SqliteDataService : ILocalDataService
    {
        private SQLiteConnection _database;

        public SqliteDataService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DiabetesDB.db3");
            Console.WriteLine(dbPath);
            try
            {
                _database = new SQLiteConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
            }
            catch(Exception ex)
            {

            }

            _database.CreateTable<Recipe>();
            _database.CreateTable<Ingredient>();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void AddItem(Recipe recipe)
        {
            if(recipe is null)
            {
                return;
            }

            CreateRecipe(recipe);
            foreach(Ingredient i in recipe.Ingredients)
            {
                i.RecipeID = recipe.ID;
                CreateIngredient(i);
            }
        }

        public void UpdateItem(Recipe recipe)
        {
            if (recipe is null)
            {
                return;
            }

            UpdateRecipe(recipe);
            foreach (Ingredient i in recipe.Ingredients)
            {
                i.RecipeID = recipe.ID;
                CreateIngredient(i);
            }
        }

        /* ------- RECIPE STUFF ------- */

        public void CreateRecipe(Recipe recipe)
        {
            _database.Insert(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _database.Update(recipe);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            _database.Delete(recipe);
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return _database.GetAllWithChildren<Recipe>();
        }

        public IEnumerable<Ingredient> GetIngredients()
        {
            return _database.Table<Ingredient>().ToList();
        }

        //public void ILocalDataService.DeleteRecipe(Recipe recipe)
        //{
        //    throw new NotImplementedException();
        //}

        /* ------- INGREDIENT STUFF ------- */

        public void CreateIngredient(Ingredient ingredient)
        {
            _database.Insert(ingredient);
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            _database.Delete(ingredient);
        }

        //public void ILocalDataService.DeleteIngredient(Ingredient ingredient)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

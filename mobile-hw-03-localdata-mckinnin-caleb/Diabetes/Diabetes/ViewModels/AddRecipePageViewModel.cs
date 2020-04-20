using Diabetes.Database;
using Diabetes.Model;
using Diabetes.Services;
using Diabetes.Services.Data;
using Diabetes.Views;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Diabetes.ViewModels
{
    public class AddRecipePageViewModel : ViewModelBase
    {
        private readonly IRecipeService _recipeService;
        private readonly ILocalDataService _localDataService;
        public ObservableCollection<Ingredient> ListOfIngredients;

        public AddRecipePageViewModel(INavigationService navigationService, IRecipeService recipeService, IPageDialogService pageDialog, ILocalDataService localDataService)
            : base(navigationService)
        {
            Title = "Add Recipe";
            _navigationService = navigationService;
            _recipeService = recipeService;
            _localDataService = localDataService;
            _pageDialog = pageDialog;

            SubmitRecipe = new Command(submitrecipe_execute, submitRecipe_canexecute);
            AddIngredient = new Command(addIngredient_execute, addIngredient_canexecute);
            recipe = new Recipe();
            db = new SqliteDataService();
            ListOfIngredients = new ObservableCollection<Ingredient>(); 
        }

        private ILocalDataService db;


        /*-----------Navigate Back To Main-----------*/

        private Command backToHome;
        public Command BackToHome => backToHome ?? (backToHome = new Command(() =>
        {
            NavigationParameters parameters = new NavigationParameters();
            _navigationService.NavigateAsync(nameof(MainPage), parameters, false, false);
        }));

        /*-----------Navigate Back To Main-----------*/


        /*-------------------add ingredient to a the list for the recipe button----------------------------*/
        private bool addIngredient_canexecute()
        {
            if (IngredientName == "")
            {
                return false;
            }
            if (IngredientName == null)
            {
                return false;
            }
            if (IngredientQuantity < 1)
            {
                return false;
            }
            return true;
        }

        private void addIngredient_execute()
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Name = IngredientName;
            ingredient.Quantity = IngredientQuantity;
            ListOfIngredients.Add(ingredient);
        }

        /*-------------------Add ingredient to a the list for the recipe button----------------------------*/







        /*-------------------Add Recipe once the test pass----------------------------*/
        private bool submitRecipe_canexecute()
        {
            if (RecipeName == null)
            {
                return false;
            }
            if (IngredientName == "")
            {
                return false;
            }
            if (IngredientName == null)
            {
                return false;
            }
            if (RecipeName.Length <= 5)
            {
                return false;
            }
            if (IngredientQuantity < 1)
            {
                return false;
            }

            return true;
        }

        public Recipe recipe { get; set; }

        public Recipe MakeRecipe()
        {
            try
            {
                Recipe recipe = new Recipe();
                recipe.Name = this.RecipeName;
                //steps
                recipe.Ingredients = new List<Ingredient>(ListOfIngredients);
                return recipe;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private async void submitrecipe_execute()
        {
      
            if (await _pageDialog.DisplayAlertAsync("Question?", "Are you sure you'd like to save this?", "Continue", "Cancel"))
            {
                
                await TestableNavigation.TestableNavigateAsync(_navigationService, "MainPage", new NavigationParameters("?action=confirm"), false, false);
                NavigationParameters parameters = new NavigationParameters();
                db.AddItem(MakeRecipe());
                parameters.Add("recipe", recipe);
                _navigationService.NavigateAsync(nameof(MainPage), parameters, false, false);
                _recipeService.SubmitRecipe(null);
            }

        }


    

        /*-------------------Add Recipe once the test pass----------------------------*/









        //Prep time 2.0

        /*------------------------------------------Whole Recipe variables------------------------------------------*/

        private string step;
        public string Step
        {
            get => step;
            set
            {
                SetProperty(ref step, value);
                //This tells my viewmodel to reevauate canexecute on a change.
                SubmitRecipe.ChangeCanExecute();
            }
        }

        private string recipeName;

        public string RecipeName
        {
            get => recipeName;
            set
            {
                SetProperty(ref recipeName, value);
                //This tells my viewmodel to reevauate canexecute on a change.
                SubmitRecipe.ChangeCanExecute();
            }
        }

        private Command submitRecipe;
        public Command SubmitRecipe
        {
            get { return submitRecipe; }
            set { submitRecipe = value; }
        }
        /*------------------------------------------Whole Recipe variables------------------------------------------*/






        /*------------------------------------------Ingredient variables------------------------------------------*/

        private string ingedientName;
        public string IngredientName
        {
            get => ingedientName;
            set
            {
                SetProperty(ref ingedientName, value);
                SubmitRecipe.ChangeCanExecute();
                AddIngredient.ChangeCanExecute();
            }
        }

        private int ingredientQuantity;
        public int IngredientQuantity
        {
            get => ingredientQuantity;
            set
            {
                SetProperty(ref ingredientQuantity, value);
                SubmitRecipe.ChangeCanExecute();
                AddIngredient.ChangeCanExecute();
            }
        }

        private Command addIngredient;

        public Command AddIngredient
        {
            get { return addIngredient; }
            set { addIngredient = value;  }
        }

        private int ingredientIdCounter { get; set; }


        /*------------------------------------------Ingredient variables------------------------------------------*/

        

        
        public INavigationService _navigationService { get; }
        public IPageDialogService _pageDialog { get; }



        private Command navigate;
        public Command Navigate => navigate ?? (navigate = new Command(async () =>
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("recipe", recipe);
            await TestableNavigation.TestableNavigateAsync(_navigationService, "MainPage", parameters, false, true).ConfigureAwait(false);
        }));

    }

}

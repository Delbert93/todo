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
    
    public class EditRecipePageViewModel : ViewModelBase
    {
        public ObservableCollection<Ingredient> ListOfIngredients;
        private readonly IRecipeService _recipeService;
        private readonly ILocalDataService _localDataService;
        public EditRecipePageViewModel(INavigationService navigationService, IRecipeService recipeService, IPageDialogService pageDialog, ILocalDataService localDataService)
            : base(navigationService)
        {
            Title = "Edit Recipe";
            _navigationService = navigationService;
            _recipeService = recipeService;
            _localDataService = localDataService;
            _pageDialog = pageDialog;

            SaveRecipe = new Command(saverecipe_execute);
            AddIngredient = new Command(addIngredient_execute);
            recipe = new Recipe();
            db = new SqliteDataService();
            ListOfIngredients = new ObservableCollection<Ingredient>();
        }

        public IPageDialogService _pageDialog { get; }

        public IEnumerable<Ingredient> Ingredients { get; set; }
        public Recipe recipe { get; set; }

        private ILocalDataService db;






        /*-----------Navigate Back To Main-----------*/

        private Command backToHome;
        public Command BackToHome => backToHome ?? (backToHome = new Command(() =>
        {
            NavigationParameters parameters = new NavigationParameters();
            _navigationService.NavigateAsync(nameof(MainPage), parameters, false, false);
        }));

        /*-----------Navigate Back To Main-----------*/







        /*---------------------------Receive single recipe from DisplaySingleRecipeViewModel--------------------*/

        private Recipe singleRecipe;
        public Recipe SingleRecipe
        {
            get => singleRecipe;
            set { SetProperty(ref singleRecipe, value); }
        }
        /// <summary>
        /// Gets recipe object from DisplaySingleRecipeViewModel
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            refresh();
            SingleRecipe = (Recipe)parameters["recipe"];
        }

        private void refresh()
        {
            Ingredients = db.GetIngredients();
        }


        /*---------------------------Receive single recipe from DisplaySingleRecipeViewModel--------------------*/







        /*---------------------------Recipe variables--------------------*/

        private string recipeName;

        public string RecipeName
        {
            get => recipeName;
            set
            {
                SetProperty(ref recipeName, value);
                //This tells my viewmodel to reevauate canexecute on a change.
                SaveRecipe.ChangeCanExecute();
            }
        }

        private Command saveRecipe;
        public Command SaveRecipe
        {
            get { return saveRecipe; }
            set { saveRecipe = value; }
        }

        /*---------------------------Recipe variables--------------------*/








        /*---------------------------Ingredient variables--------------------*/

        private Command addIngredient;

        public Command AddIngredient
        {
            get { return addIngredient; }
            set { addIngredient = value; }
        }

        private string ingredientName;

        public string IngredientName
        {
            get => ingredientName;
            set
            {
                SetProperty(ref ingredientName, value);
                //This tells my viewmodel to reevauate canexecute on a change.
                SaveRecipe.ChangeCanExecute();
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
                SaveRecipe.ChangeCanExecute();
                AddIngredient.ChangeCanExecute();
            }
        }


        /*---------------------------Ingredient variables--------------------*/







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








        /*---------------Save button turns on once test pass--------------------*/

        /// <summary>
        ///     this grays out the button until the test pass.
        /// </summary>
        /// <returns></returns>
        private bool saveRecipe_canexecute()
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

       

        public Recipe MakeRecipe()
        {
            try
            {

                SingleRecipe.Name = this.RecipeName;
                //steps
                SingleRecipe.Ingredients = new List<Ingredient>(ListOfIngredients);
                return SingleRecipe;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async void saverecipe_execute()
        {

            if (await _pageDialog.DisplayAlertAsync("Question?", "Are you sure you'd like to save this?", "Continue", "Cancel"))
            {

                await TestableNavigation.TestableNavigateAsync(_navigationService, "MainPage", new NavigationParameters("?action=confirm"), false, false);
                NavigationParameters parameters = new NavigationParameters();
                db.UpdateItem(MakeRecipe());
                parameters.Add("recipe", SingleRecipe);
                _navigationService.NavigateAsync(nameof(MainPage), parameters, false, false);
                _recipeService.SaveRecipe(null);
            }

        }

        /*---------------Save button turns on once test pass--------------------*/








        /*-----------Navigate Back To Main-----------*/

        private Command foodPage;
        public Command FoodPage
        {
            get { return foodPage; }
            set { foodPage = value; }
        }


        private Recipe selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set { SetProperty(ref selectedRecipe, value); }
        }


        private Command viewRecipeCommand;
        public Command ViewRecipeCommand => viewRecipeCommand ?? (viewRecipeCommand = new Command(() =>
        {
            NavigationParameters parameters = new NavigationParameters();
            var singleRecipe = SingleRecipe;
            parameters.Add("recipe", singleRecipe);
            _navigationService.NavigateAsync(nameof(DisplaySingleRecipe), parameters, false, false);
        }));

        /*-----------Navigate Back To Main-----------*/


        public INavigationService _navigationService { get; }

    }
}

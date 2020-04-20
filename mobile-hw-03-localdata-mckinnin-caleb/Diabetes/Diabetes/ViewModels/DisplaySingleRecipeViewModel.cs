using Diabetes.Database;
using Diabetes.Model;
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
    public class DisplaySingleRecipeViewModel : ViewModelBase
    {
        public DisplaySingleRecipeViewModel(INavigationService navigationService, IRecipeService recipeService, IPageDialogService pageDialog, ILocalDataService localDataService)
            : base(navigationService)
        {
            recipe = new Recipe();
            _pageDialog = pageDialog;
            _navigationService = navigationService;

            DeleteRecipe = new Command(deleterecipe_execute);
            db = new SqliteDataService();
        }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public Recipe recipe { get; set; }
        public INavigationService _navigationService { get; }
        public IPageDialogService _pageDialog { get; }

        private ILocalDataService db;

        private Recipe singleRecipe;
        public Recipe SingleRecipe
        {
            get => singleRecipe;
            set { SetProperty(ref singleRecipe, value); }
        }


        private string title;
        public string Title
        {
            get => title;
            set { SetProperty(ref title, value); }
        }

        //public string RecipeImage = "pizza.png";




        /*-----------Navigate Back To Main-----------*/

        private Command backToHome;
        public Command BackToHome => backToHome ?? (backToHome = new Command(() =>
       {
           NavigationParameters parameters = new NavigationParameters();
           _navigationService.NavigateAsync(nameof(MainPage), parameters, false, false);
       }));
       
        /*-----------Navigate Back To Main-----------*/






        /*-----------------OnNavigatTo from main page-----------------------*/

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            refresh();
            SingleRecipe = (Recipe)parameters["recipe"];
            Title = SingleRecipe.Name;
        }

        private void refresh()
        {
            Ingredients = db.GetIngredients();
        }


        /*-----------------OnNavigatTo from main page-----------------------*/







        /*-----------------Delete recipe-----------------*/

        private Command deleteRecipe;

        public Command DeleteRecipe
        {
            get { return deleteRecipe; }
            set { deleteRecipe = value; }
        }

        private Recipe selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set { SetProperty(ref selectedRecipe, value); }
        }
        
        private async void deleterecipe_execute()
        {

            if (await _pageDialog.DisplayAlertAsync("Question?", "Are you sure you'd like to delete this?", "Continue", "Cancel"))
            {

                //await TestableNavigation.TestableNavigateAsync(_navigationService, "MainPage", new NavigationParameters("?action=confirm"), false, false);
                NavigationParameters parameters = new NavigationParameters();

                db.DeleteRecipe(SingleRecipe);
                _navigationService.NavigateAsync(nameof(MainPage), null, false, false);
                //_recipeService.SubmitRecipe(null);
            }
        }

        /*-----------------Delete recipe-----------------------*/







        /*-----------------Edit recipe-----------------------*/

        private Command editRecipe;
        public Command EditRecipe => editRecipe ?? (editRecipe = new Command(() =>
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("recipe", SingleRecipe);
            _navigationService.NavigateAsync(nameof(EditRecipePage), parameters, false, false);
        }));

        /*-----------------Edit recipe-----------------------*/
    }
}

using Diabetes.Database;
using Diabetes.Model;
using Diabetes.Services;
using Diabetes.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace Diabetes.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            Title = "Main Page";
            AddRecipe = new Command(navigate_execute);
            //FoodPage = new Command(navigate_can_execute);

            db = new SqliteDataService();
            Recipes = db.GetRecipes();
        }

        public Recipe recipe { get; set; }

        private ILocalDataService db;

        public IEnumerable<Recipe> Recipes { get; set; }

        public ImageSource AppLogo => ImageSource.FromResource("Diabetes.Images.applogo.png");


        /*-----------------Navigation to add recipe-----------------------*/
        private async void navigate_execute()
        {
            await _navigationService.NavigateAsync(nameof(Diabetes.Views.AddRecipePage));
        }

        private Command addRecipe;
        public Command AddRecipe
        {
            get { return addRecipe; }
            set { addRecipe = value; }

        }
        /*-----------------Navigation to add recipe-----------------------*/





        /*-----------------OnNavigatTo from add recipe-----------------------*/

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Recipes = db.GetRecipes();
        }

        /*-----------------OnNavigatTo from add recipe-----------------------*/


        /*-----------------Navigate To Food page-----------------------*/
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
            var singleRecipe = SelectedRecipe;
            parameters.Add("recipe", singleRecipe);
            _navigationService.NavigateAsync(nameof(DisplaySingleRecipe), parameters, false, false);
        }));

        /*-----------------Navigate To Food page-----------------------*/
    }
}

using Prism;
using Prism.Ioc;
using Diabetes.ViewModels;
using Diabetes.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diabetes.Services;
using Diabetes.Services.Data;
using Diabetes.Database;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Diabetes
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRecipeService, SqliteRecipeService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<EditRecipePage, EditRecipePageViewModel>();
            containerRegistry.RegisterForNavigation<AddRecipePage, AddRecipePageViewModel>();
            containerRegistry.RegisterForNavigation<DisplaySingleRecipe, DisplaySingleRecipeViewModel>();
            containerRegistry.Register<ILocalDataService, SqliteDataService>();
        }
    }
}

using Diabetes.Database;
using Diabetes.Services;
using Diabetes.Services.Data;
using Diabetes.ViewModels;
using Moq;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using INavigationService = Prism.Navigation.INavigationService;

namespace BloodSugarTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

            

        }
        /// <summary>
        /// Cant add recipe if the recipe name is shorter then 6 chars
        /// </summary>
        [Test]
        public void CanNotAddRecipeWithShortRecipeName()
        {
            // A mock test behavior in a view model and it requires an interface like inavigation and since we dont want to give it the real one mock makes it think that it does have an implementation of that interface. 
            var navaMock = new Mock<INavigationService>();



            var recipeServiceMock = new Mock<IRecipeService>();
            var dbMock = new Mock<ILocalDataService>();
            var pageMock = new Mock<IPageDialogService>();



            var editViewModel = new Diabetes.ViewModels.EditRecipePageViewModel(navaMock.Object, recipeServiceMock.Object, pageMock.Object, dbMock.Object);
            editViewModel.RecipeName = "short";



            var addViewModel = new Diabetes.ViewModels.AddRecipePageViewModel(navaMock.Object, recipeServiceMock.Object, pageMock.Object, dbMock.Object);
            addViewModel.RecipeName = "short";


          

            //Assert.IsFalse(editViewModel.SubmitRecipe.CanExecute(this));
            Assert.IsFalse(addViewModel.SubmitRecipe.CanExecute(this));

        }






        /// <summary>
        /// Allows recipe to be added now that there is a name with at least 6 chars.
        /// </summary>
        [Test]
        public void CanAddRecipeWithLongRecipeName()
        {
            var navaMock = new Mock<INavigationService>();

            var recipeServiceMock = new Mock<IRecipeService>(); 
            var dbMock = new Mock<ILocalDataService>();
            var pageMock = new Mock<IPageDialogService>();



            var editViewModel = new Diabetes.ViewModels.EditRecipePageViewModel(navaMock.Object, recipeServiceMock.Object, pageMock.Object, dbMock.Object);
            editViewModel.RecipeName = "something long";
            editViewModel.IngredientName = "sugar";



            var addViewModel = new Diabetes.ViewModels.AddRecipePageViewModel(navaMock.Object, recipeServiceMock.Object, pageMock.Object, dbMock.Object);
            addViewModel.RecipeName = "something long";
            addViewModel.IngredientName = "sugar";
            addViewModel.IngredientQuantity = 4;



            Assert.IsTrue(editViewModel.SaveRecipe.CanExecute(this), "Edit Failed");
            Assert.IsTrue(addViewModel.SubmitRecipe.CanExecute(this), "Add Failed");

        }






        /// <summary>
        /// //Unit test that requires a recipe cannot be added without ingredients
        /// </summary>
        [Test]
        public void CanNotAddRecipeWithOutIngredient() 
        {
            var navaMock = new Mock<INavigationService>();



            var recipServiceMock = new Mock<IRecipeService>();
            var dbMock = new Mock<ILocalDataService>();
            var pageMock = new Mock<IPageDialogService>();



            var editViewModel = new Diabetes.ViewModels.EditRecipePageViewModel(navaMock.Object, recipServiceMock.Object, pageMock.Object, dbMock.Object);
            editViewModel.IngredientName = "";



            var addViewModel = new Diabetes.ViewModels.AddRecipePageViewModel(navaMock.Object, recipServiceMock.Object, pageMock.Object, dbMock.Object);
            addViewModel.IngredientName = "";
            addViewModel.IngredientQuantity = 0;



            Assert.IsFalse(editViewModel.SaveRecipe.CanExecute(this), "Edit Failed");
            Assert.IsFalse(addViewModel.SubmitRecipe.CanExecute(this), "Add Failed");
        }






        /// <summary>
        /// //Unit test that alows a recipe tp be added once there is a ingredient
        /// </summary>
        [Test]
        public void CanAddRecipeWithIngredient()
        {
            var navaMock = new Mock<INavigationService>();



            var recipServiceMock = new Mock<IRecipeService>();
            var dbMock = new Mock<ILocalDataService>();
            var pageMock = new Mock<IPageDialogService>();



            var editViewModel = new Diabetes.ViewModels.EditRecipePageViewModel(navaMock.Object, recipServiceMock.Object, pageMock.Object, dbMock.Object);
            editViewModel.RecipeName = "something long";
            editViewModel.IngredientName = "sugar";



            var addViewModel = new Diabetes.ViewModels.AddRecipePageViewModel(navaMock.Object, recipServiceMock.Object, pageMock.Object, dbMock.Object);
            addViewModel.RecipeName = "something long";
            addViewModel.IngredientName = "sugar";
            addViewModel.IngredientQuantity = 3;



            Assert.IsTrue(editViewModel.SaveRecipe.CanExecute(this), "Edit Failed");
            Assert.IsTrue(addViewModel.SubmitRecipe.CanExecute(this), "Add Failed");
        }





        [Test]
        public void TestSave()
        {
            var pageDialogServiceMock = new Mock<IPageDialogService>();
            pageDialogServiceMock.Setup(m => m.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), 
                It.IsAny<string>())).Returns(Task.FromResult(true));

            var navaMock = new Mock<INavigationService>();
            //int numberOfCalls = 0;
            INavigationParameters actualNavParams = null;

            var addRecipePage = new AddRecipePageViewModel(navaMock.Object, null, pageDialogServiceMock.Object, null);
            TestableNavigation.TestableNavigateAsync = (navSvc, addRecipePage, navParams, isModal, isAnimated) =>
            {
                //++numberOfCalls;
                actualNavParams = navParams;
                return Task.FromResult<INavigationResult>(null);
            };

            
            //addRecipePage.Navigate();
            //addRecipePage.Navigate(new NavigationParameters("?action=confirm"));

            //Assert.AreEqual(1, numberOfCalls, "number is incorrect");
            Assert.AreEqual("confirm", actualNavParams["confirm"]);

        }


        /*---------------------extra code------------------------*/
            //var navaMock = new Mock<INavigationService>();
            //var recipeServiceMock = new Mock<IRecipeService>();
            //var addViewModel = new Diabetes.ViewModels.AddRecipePageViewModel(navaMock.Object, recipeServiceMock.Object);

            //addViewModel.RecipeName = "short";
            //addViewModel.RecipeName = "something long";
            //addViewModel.IngredientName = "sugar";
            //addViewModel.IngedientQuantity = 1;
        /*---------------------extra code------------------------*/

    }
}
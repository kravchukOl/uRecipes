using static System.Net.Mime.MediaTypeNames;
using uRecipes.Views;
using uRecipes.Views.Login;
using uRecipes.Views.Demo;
using uRecipes.ViewModels;
using uRecipes.Services.LocalRepository;
using uRecipes.ViewModels.Demo;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;

namespace uRecipes;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("Leto Text Sans Defect.otf", "LetoTextSans");
            fonts.AddFont("helvetica_regular.otf", "HelveticaRegular");
            fonts.AddFont("Helvetica-Bold.otf", "HelveticaBold");
            // icon Fonts:
            fonts.AddFont("uRecipe.ttf", "uRecipeIconFont");
        }).UseMauiCommunityToolkit();

        // Services registrations:
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        //builder.Services.AddSingleton(Shell.Current); 
        builder.Services.AddSingleton<INavigator, PageNavigator>();
        builder.Services.AddSingleton<IRecipeLocalRepository, RecipeLocalRepository>();

        // ViewModel registrations:
        builder.Services.AddSingleton<FavoritesViewModel>();
        builder.Services.AddSingleton<ChefViewModel>();
        builder.Services.AddSingleton<ExploreViewModel>();

        builder.Services.AddTransient<RecipeViewModel>();

        // Views Registrations
        builder.Services.AddSingleton<FavoritesPage>();
        builder.Services.AddSingleton<ExplorePage>();
        builder.Services.AddSingleton<ChefPage>();

        builder.Services.AddTransient<RecipePage>();

        // Test Pages registrations:
        builder.Services.AddTransient<AddRecipePage>();
        // Test ViewModels registrations:
        builder.Services.AddTransient<AddRecipeViewModel>();

        return builder.Build();
    }
}
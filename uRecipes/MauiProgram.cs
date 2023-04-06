using static System.Net.Mime.MediaTypeNames;
using uRecipes.Views;
using uRecipes.Views.Login;
using uRecipes.Views.Demo;
using uRecipes.ViewModels;
using uRecipes.Services.LocalRepository;
using uRecipes.Services.LocalisationSevice;
using uRecipes.ViewModels.Demo;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Handlers;
using Sharpnado.Tabs;

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
        })
            .UseMauiCommunityToolkit()
            .UseSharpnadoTabs(loggerEnable: false);

        // Services registrations:
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<ILocalizationResourceManager,LocalizationResourceManager>();

        //builder.Services.AddSingleton(Shell.Current); 
        builder.Services.AddSingleton<INavigator, PageNavigator>();
        builder.Services.AddSingleton<IRecipeLocalRepository, RecipeLocalRepository>();

        // ViewModel registrations:
        builder.Services.AddSingleton<FavoritesViewModel>();
        builder.Services.AddSingleton<ChefViewModel>();
        builder.Services.AddSingleton<ExploreViewModel>();
        builder.Services.AddSingleton<DeviceViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
      
        builder.Services.AddTransient<RecipeViewModel>();

        // Pages Registrations
        builder.Services.AddSingleton<FavoritesPage>();
        builder.Services.AddSingleton<ExplorePage>();
        builder.Services.AddSingleton<ChefPage>();
        builder.Services.AddSingleton<DevicePage>();
        builder.Services.AddSingleton<SettingsPage>();

        builder.Services.AddTransient<RecipePage>();

        // Test Pages registrations:
        builder.Services.AddTransient<AddRecipePage>();
        // Test ViewModels registrations:
        builder.Services.AddTransient<AddRecipeViewModel>();



        return builder.Build();
    }
}
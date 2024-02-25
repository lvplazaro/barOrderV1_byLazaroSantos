using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using barOrderV1.View;
using barOrderV1.ViewModel;
using barOrderV1.Services;
using CommunityToolkit.Maui;

namespace barOrderV1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<PaginaInicialView>();
            builder.Services.AddSingleton<PaginaInicialViewModel>();

            builder.Services.AddTransient<EstoqueView>();
            builder.Services.AddTransient<EstoqueViewModel>();

            builder.Services.AddTransient<EstoqueDetailsView>();
            builder.Services.AddTransient<EstoqueDetailsViewModel>();

            builder.Services.AddTransient<AddProdutoView>();
            builder.Services.AddTransient<AddProdutoViewModel>();

            builder.Services.AddTransient<EditProdutoView>();
            builder.Services.AddTransient<EditProdutoViewModel>();




#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IProdutoService, ProdutoService>();

            return builder.Build();
        }

        
    }

}

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using barOrderV1.View;
using barOrderV1.ViewModel;
using barOrderV1.Services;
using CommunityToolkit.Maui;
using barOrderV1.View.Comandas;
using barOrderV1.ViewModel.Comandas;
using barOrderV1.Model;
using barOrderV1.Model.Enums;

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

            builder.Services.AddTransient<ComandaAbertaView>();
            builder.Services.AddTransient<ComandaAbertaViewModel>();

            builder.Services.AddTransient<ProdutosPopUpView, ProdutosPopUpViewModel>();
            builder.Services.AddTransient<FechamentoDeComandaView, FechamentoDeComandaViewModel>();

            builder.Services.AddTransient<ComandasFechadasView, ComandasFechadasViewModel>();




#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IProdutoService, ProdutoService>();
            builder.Services.AddSingleton<IComandaService, ComandaService>();
            builder.Services.AddSingleton<IComandaProdutoService, ComandaProdutoService>();


            return builder.Build();
        }

        
    }

}

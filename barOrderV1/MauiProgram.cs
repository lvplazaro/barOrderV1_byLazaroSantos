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
using barOrderV1.View.Outros;
using barOrderV1.ViewModel.Outros;
using barOrderV1.View.Relatorios;
using barOrderV1.ViewModel.Relatorios;

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
            builder.Services.AddTransient<ComandaFechadaView, ComandaFechadaViewModel>();
            builder.Services.AddTransient<ComandasFechadasView, ComandasFechadasViewModel>();

            builder.Services.AddTransient<AjudaView>();
            builder.Services.AddTransient<SobreView>();



            builder.Services.AddTransient<ConfigView, ConfigViewModel>();
            builder.Services.AddTransient<LoginView, LoginViewModel>();

            builder.Services.AddTransient<RelatoriosView, RelatoriosViewModel>();


            builder.Services.AddTransient<RelatorioFaturamentoView, RelatorioFaturamentoViewModel>();
            builder.Services.AddTransient<RelatorioProdutosVendidosView, RelatorioProdutosVendidosViewModel>();
            builder.Services.AddTransient<RelatorioFaturamentoEsperadoView, RelatorioFaturamentoEsperadoViewModel>();
            builder.Services.AddTransient<RelatorioEstoqueCriticoView, RelatorioEstoqueCriticoViewModel>();






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

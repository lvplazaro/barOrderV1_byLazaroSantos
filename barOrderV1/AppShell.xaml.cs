using barOrderV1.View;
using barOrderV1.View.Comandas;
using barOrderV1.View.Outros;
using barOrderV1.View.Relatorios;

namespace barOrderV1
{
    public partial class AppShell : Shell
    {
        public AppShell(ServiceProvider serviceProvider)
        {
            InitializeComponent();

            var getuserSavedkey = Preferences.Get("UserAlreadyLoggedIn", false);

            if (getuserSavedkey == true)
            {
                MyAppShell.CurrentItem = MyMainPage;
            }
            else
            {
                MyAppShell.CurrentItem = MyLoginPage;

            }


            Routing.RegisterRoute(nameof(EditProdutoView), typeof(EditProdutoView));
            Routing.RegisterRoute(nameof(EstoqueDetailsView), typeof(EstoqueDetailsView));
            Routing.RegisterRoute(nameof(AddProdutoView), typeof(AddProdutoView));

            Routing.RegisterRoute(nameof(ProdutosPopUpView), typeof(ProdutosPopUpView));
            Routing.RegisterRoute(nameof(ComandaAbertaView), typeof(ComandaAbertaView));
            Routing.RegisterRoute(nameof(FechamentoDeComandaView), typeof(FechamentoDeComandaView));
            Routing.RegisterRoute(nameof(ComandasFechadasView), typeof(ComandasFechadasView));
            Routing.RegisterRoute(nameof(ComandaFechadaView), typeof(ComandaFechadaView));
            Routing.RegisterRoute(nameof(AjudaView), typeof(AjudaView));
            Routing.RegisterRoute(nameof(SobreView), typeof(SobreView));

            Routing.RegisterRoute(nameof(RelatoriosView), typeof(RelatoriosView));

            Routing.RegisterRoute(nameof(RelatorioFaturamentoView), typeof(RelatorioFaturamentoView));
            Routing.RegisterRoute(nameof(RelatorioProdutosVendidosView), typeof(RelatorioProdutosVendidosView));
            Routing.RegisterRoute(nameof(RelatorioFaturamentoEsperadoView), typeof(RelatorioFaturamentoEsperadoView));
            Routing.RegisterRoute(nameof(RelatorioEstoqueCriticoView), typeof(RelatorioEstoqueCriticoView));





            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(ConfigView), typeof(ConfigView));





        }
    }
}

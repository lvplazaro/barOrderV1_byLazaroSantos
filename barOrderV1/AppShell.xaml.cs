using barOrderV1.View;
using barOrderV1.View.Comandas;

namespace barOrderV1
{
    public partial class AppShell : Shell
    {
        public AppShell(ServiceProvider serviceProvider)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EditProdutoView), typeof(EditProdutoView));
            Routing.RegisterRoute(nameof(EstoqueDetailsView), typeof(EstoqueDetailsView));
            Routing.RegisterRoute(nameof(AddProdutoView), typeof(AddProdutoView));

            Routing.RegisterRoute(nameof(AddComandaView), typeof(AddComandaView));

        }
    }
}

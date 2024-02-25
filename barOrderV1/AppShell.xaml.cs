using barOrderV1.View;

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

        }
    }
}

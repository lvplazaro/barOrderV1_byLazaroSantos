using barOrderV1.View;

namespace barOrderV1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(EstoqueDetailsView),typeof(EstoqueDetailsView));
        }
    }
}

using barOrderV1.Services;

namespace barOrderV1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();

            services.AddSingleton<IProdutoService, ProdutoService>();
            services.AddSingleton<IComandaService, ComandaService>();
            services.AddSingleton<IComandaProdutoService, ComandaProdutoService>();


            var serviceProvider = services.BuildServiceProvider();

            MainPage = new AppShell(serviceProvider);
        }
    }
}

using barOrderV1.Services;

namespace barOrderV1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();

            // Registre as interfaces e suas implementações
            services.AddSingleton<IProdutoService, ProdutoService>();
            services.AddSingleton<IComandaService, ComandaService>();


            var serviceProvider = services.BuildServiceProvider();

            MainPage = new AppShell(serviceProvider);
        }
    }
}

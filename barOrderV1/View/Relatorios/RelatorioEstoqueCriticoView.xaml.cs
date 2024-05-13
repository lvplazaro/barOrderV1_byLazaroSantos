using barOrderV1.ViewModel.Relatorios;

namespace barOrderV1.View.Relatorios;

public partial class RelatorioEstoqueCriticoView : ContentPage
{
	public RelatorioEstoqueCriticoView(RelatorioEstoqueCriticoViewModel relatorioEstoqueCriticoViewModel)
	{
		InitializeComponent();
		BindingContext = relatorioEstoqueCriticoViewModel;
	}
}
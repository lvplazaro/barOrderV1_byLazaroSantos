using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class RelatorioView : ContentPage
{
	public RelatorioView(RelatorioViewModel relatorioViewModel)
	{
		InitializeComponent();
		BindingContext = relatorioViewModel;
	}
}



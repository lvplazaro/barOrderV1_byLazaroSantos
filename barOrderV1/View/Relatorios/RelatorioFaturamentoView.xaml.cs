using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class RelatorioFaturamentoView : ContentPage
{
	public RelatorioFaturamentoView(RelatorioFaturamentoViewModel relatorioViewModel)
	{
		InitializeComponent();
		BindingContext = relatorioViewModel;
	}
}



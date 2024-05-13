using barOrderV1.ViewModel.Relatorios;

namespace barOrderV1.View.Relatorios;

public partial class RelatorioFaturamentoEsperadoView : ContentPage
{
	public RelatorioFaturamentoEsperadoView(RelatorioFaturamentoEsperadoViewModel relatorioFaturamentoEsperadoViewModel)
	{
		InitializeComponent();
		BindingContext = relatorioFaturamentoEsperadoViewModel;

    }
}
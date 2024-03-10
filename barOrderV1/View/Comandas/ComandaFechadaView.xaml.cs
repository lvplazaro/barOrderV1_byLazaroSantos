using barOrderV1.ViewModel.Comandas;

namespace barOrderV1.View.Comandas;

public partial class ComandaFechadaView : ContentPage
{
	public ComandaFechadaView(ComandaFechadaViewModel comandaFechadaViewModel)
	{
		InitializeComponent();
		BindingContext = comandaFechadaViewModel;
	}
}

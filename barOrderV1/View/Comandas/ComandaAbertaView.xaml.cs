using barOrderV1.ViewModel.Comandas;

namespace barOrderV1.View.Comandas;

public partial class ComandaAbertaView : ContentPage
{
	public ComandaAbertaView(ComandaAbertaViewModel comandaAbertaViewModel)
	{
		InitializeComponent();
		BindingContext = comandaAbertaViewModel;
	}
}

using barOrderV1.ViewModel;
using barOrderV1.ViewModel.Comandas;

namespace barOrderV1.View.Comandas;

public partial class AddComandaView : ContentPage
{
	public AddComandaView(AddComandaViewModel addComandaViewModel)
	{
		InitializeComponent();
		BindingContext = addComandaViewModel;
	}
}
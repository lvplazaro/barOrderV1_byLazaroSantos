using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class EstoqueView : ContentPage
{
	public EstoqueView()
	{
		InitializeComponent();
		BindingContext = new EstoqueViewModel();
	}
}
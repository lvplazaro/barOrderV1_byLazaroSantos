using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class PaginaInicialView : ContentPage
{
	public PaginaInicialView()
	{
		InitializeComponent();
		BindingContext = new PaginaInicialViewModel();
	}
}
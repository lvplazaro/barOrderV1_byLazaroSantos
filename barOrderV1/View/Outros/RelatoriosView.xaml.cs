using barOrderV1.ViewModel.Outros;

namespace barOrderV1.View.Outros;

public partial class RelatoriosView : ContentPage
{
	public RelatoriosView()
	{
		InitializeComponent();
		BindingContext = new RelatoriosViewModel();
	}
}
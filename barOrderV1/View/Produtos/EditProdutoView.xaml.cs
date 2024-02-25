using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class EditProdutoView : ContentPage
{
	public EditProdutoView(EditProdutoViewModel editProdutoViewModel)
	{
		InitializeComponent();
		BindingContext = editProdutoViewModel;

    }
}


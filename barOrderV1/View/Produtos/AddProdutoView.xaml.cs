using barOrderV1.Services;
using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class AddProdutoView : ContentPage
{
	public AddProdutoView(AddProdutoViewModel addProdutoViewModel)
	{
		InitializeComponent();
        

        BindingContext = addProdutoViewModel;
    }
}
using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.ViewModel;

namespace barOrderV1.View;

public partial class EstoqueDetailsView : ContentPage
{
    public EstoqueDetailsView()
    {
        InitializeComponent();
        

        BindingContext = new EstoqueDetailsViewModel();
    }
}

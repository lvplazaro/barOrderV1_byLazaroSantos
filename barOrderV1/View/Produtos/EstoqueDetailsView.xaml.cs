using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.ViewModel;
using System.Collections.ObjectModel;

namespace barOrderV1.View;

public partial class EstoqueDetailsView : ContentPage
{
    public EstoqueDetailsView(EstoqueDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}




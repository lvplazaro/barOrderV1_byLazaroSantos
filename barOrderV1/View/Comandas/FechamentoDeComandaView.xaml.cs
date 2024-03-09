using barOrderV1.ViewModel.Comandas;

namespace barOrderV1.View.Comandas;

public partial class FechamentoDeComandaView : ContentPage
{
    private readonly FechamentoDeComandaViewModel _fechamentoDeComandaViewModel;
    public FechamentoDeComandaView(FechamentoDeComandaViewModel fechamentoDeComandaViewModel)
    {
        InitializeComponent();
        _fechamentoDeComandaViewModel = fechamentoDeComandaViewModel;
        BindingContext = _fechamentoDeComandaViewModel;


    }

    
}

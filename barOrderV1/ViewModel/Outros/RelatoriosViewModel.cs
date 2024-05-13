using barOrderV1.View;
using barOrderV1.View.Relatorios;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.ViewModel.Outros
{
    public partial class RelatoriosViewModel : BaseViewModel
    {
        public RelatoriosViewModel()
        {
            
        }


        [RelayCommand]
        Task IrParaRFaturamento()
        {
            return Shell.Current.GoToAsync(nameof(RelatorioFaturamentoView));
        }

        [RelayCommand]
        Task IrParaRProdutosVendidos()
        {
            return Shell.Current.GoToAsync(nameof(RelatorioProdutosVendidosView));
        }

        [RelayCommand]
        Task IrParaRFaturamentoEsperado()
        {
            return Shell.Current.GoToAsync(nameof(RelatorioFaturamentoEsperadoView));
        }

        

        [RelayCommand]
        Task IrParaREstoqueCritico()
        {
            return Shell.Current.GoToAsync(nameof(RelatorioEstoqueCriticoView));
        }
    }
}

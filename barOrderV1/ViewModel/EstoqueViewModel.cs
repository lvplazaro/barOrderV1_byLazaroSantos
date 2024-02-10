using barOrderV1.Model.Enums;
using barOrderV1.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace barOrderV1.ViewModel
{
    public partial class EstoqueViewModel : BaseViewModel
    {

        [ObservableProperty]
        public CategoriaProduto _catSelecionada;


        [RelayCommand]
        Task IrParaItens(CategoriaProduto CatSelecionada) =>
            Shell.Current.GoToAsync($"{nameof(EstoqueDetailsView)}?catSelecionada={CatSelecionada}");







    }





}
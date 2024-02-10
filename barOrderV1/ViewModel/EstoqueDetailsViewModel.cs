using barOrderV1.Model;
using barOrderV1.Model.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace barOrderV1.ViewModel
{
    //[QueryProperty(nameof(NomeDaPropriedadeRecebida), "NomeDoParametroDeConsulta")]
    [QueryProperty(nameof(CategoriaSelecionada), "catSelecionada")]

    public partial class EstoqueDetailsViewModel : BaseViewModel
    {

        [ObservableProperty]
        public string? _categoriaSelecionada;


    }







}





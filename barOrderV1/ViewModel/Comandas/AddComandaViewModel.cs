using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.ViewModel.Comandas
{
    public partial class AddComandaViewModel : BaseViewModel
    {
        private readonly IComandaService _comandaService;

        public ComandaModel ComandaModel { get; private set; }

        public AddComandaViewModel(IComandaService comandaService)
        {
            _comandaService = comandaService;
        }

        [ObservableProperty]
        public string _nome;

        [ObservableProperty]
        public DateTime _dataAbertura;

        [ObservableProperty]
        public DateTime _dataFechamento;

        //public ObservableCollection<ProdutoModel>? Produtos { get; set; } = new ObservableCollection<ProdutoModel>();

        [ObservableProperty]
        public double _total;

        [ObservableProperty]
        public FormaPagamento _formaDePagamento;

        [ObservableProperty]
        public ComandaStatus _status;


        [RelayCommand]
        public async Task AddComanda()
        {
            try
            {
                var novaComanda = new ComandaModel
                {
                    Nome = Nome,
                    DataAbertura = DateTime.Now,
                    DataFechamento = DateTime.Now.AddDays(2),
                    //Produtos = null,
                    Total = 00.00,
                    FormaDePagamento = 0,
                    Status = 0,
                };
                if (string.IsNullOrEmpty(novaComanda.Nome))
                {
                    await Shell.Current.DisplayAlert("Erro", "Informe o nome do cliente", "Ok");
                    return;
                }
                
                   
                      
                    

                
                await _comandaService.InitializeAsync();
                await _comandaService.AddComanda(novaComanda);

                await Shell.Current.DisplayAlert("Sucesso", "Comanda adicionada com sucesso!", "Ok");

                MessagingCenter.Send(this, "NovaComandaAdicionada");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "ok");
            }
        }

    }
}

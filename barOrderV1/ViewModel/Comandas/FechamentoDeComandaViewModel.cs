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
    public partial class FechamentoDeComandaViewModel : BaseViewModel
    {
        private readonly IComandaProdutoService? _comandaProdutoService;
        private readonly IProdutoService? _produtoService;
        private readonly IComandaService? _comandaService;


        [ObservableProperty]
        public ComandaModel? _comandaFechamento;

        [ObservableProperty]
        private double _total;

        [ObservableProperty]
        public FormaPagamento _selectedFormaPagamento;

        private double _valorRecebido;
        public double ValorRecebido
        {
            get => _valorRecebido;
            set
            {
                SetProperty(ref _valorRecebido, value);
                CalculoDeTroco();
            }
        }


        [ObservableProperty]
        private double _trocoDoCliente;

        public ObservableCollection<ComandaProduto> ComandaProdutoAdicionados { get; set; } = new ObservableCollection<ComandaProduto>();
        public ObservableCollection<ProdutoModel> ProdutosNaComanda { get; set; } = new ObservableCollection<ProdutoModel>();

        public FechamentoDeComandaViewModel(IComandaService comandaService, IProdutoService produtoService, IComandaProdutoService comandaProdutoService, ComandaModel comandaEditavel)
        {
            ComandaFechamento = comandaEditavel;
            _comandaProdutoService = comandaProdutoService;
            _comandaService = comandaService;
            _produtoService = produtoService;
            Task.Run(GetComandaProdutosAsync);

        }

        public FechamentoDeComandaViewModel()
        {

        }

        public IList<FormaPagamento> CategoriasList => Enum.GetValues(typeof(FormaPagamento))
                                                     .Cast<FormaPagamento>()
                                                     .Where(fp => fp != FormaPagamento.Definir)
                                                     .ToList();


        [RelayCommand]
        public async Task GetComandaProdutosAsync()
        {
            try
            {
                await _comandaService.InitializeAsync();
                await _comandaProdutoService.InitializeAsync();
                await _produtoService.InitializeAsync();

                var comandasProdutos = await _comandaProdutoService.GetComandaProdutos();

                ProdutosNaComanda.Clear();
                ComandaProdutoAdicionados.Clear();

                foreach (var comandaProduto in comandasProdutos)
                {
                    if (comandaProduto.ComandaId == ComandaFechamento.Id)
                    {
                        ComandaProdutoAdicionados.Add(comandaProduto);

                        var produto = await _produtoService.GetProdutoPorId(comandaProduto.ProdutoId);

                        if (produto != null)
                        {
                            ProdutosNaComanda.Add(produto);

                            produto.QuantidadeDeProduto = comandaProduto.QuantidadeDeProduto;
                        }
                    }
                }

                ComandaFechamento.DataFechamento = DateTime.Now;


                await _comandaService.UpdateComanda(ComandaFechamento);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter produtos de comanda: {ex.Message}");
            }
        }

        public void CalculoDeTroco()
        {
            if (SelectedFormaPagamento == FormaPagamento.Dinheiro)
            {
                TrocoDoCliente = ValorRecebido - ComandaFechamento.Total;
            }
            else
            {
                TrocoDoCliente = 0;
            }
        }

        [RelayCommand]
        public async Task FinalizarAtendimento(ComandaModel ComandaFinalizada)
        {
            try
            {
                await _comandaService.InitializeAsync();

                if(SelectedFormaPagamento == null || SelectedFormaPagamento == FormaPagamento.Definir)
                {
                    await Shell.Current.DisplayAlert("Erro", "Selecione a forma de pagamento!", "Ok");

                    return;

                }

                ComandaFinalizada.FormaDePagamento = SelectedFormaPagamento;
                ComandaFinalizada.Status = ComandaStatus.Fechada;

                await _comandaService.UpdateComanda(ComandaFinalizada);

                await Shell.Current.DisplayAlert("Sucesso", "Atendimento finalizado!", "Ok");

                await Shell.Current.GoToAsync("../..");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao finalizar atendimento: {ex.Message}");
            }

        }
    }
}

using barOrderV1.Model;
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
    public partial class ComandaFechadaViewModel : BaseViewModel
    {
        private readonly IComandaService _comandaService;
        private readonly IComandaProdutoService _comandaProdutoService;
        private readonly IProdutoService _produtoService;

        public ComandaFechadaViewModel(IComandaService comandaService, IProdutoService produtoService, IComandaProdutoService comandaProdutoService, ComandaModel comandaFinalizada)
        {
            _produtoService = produtoService;
            _comandaService = comandaService;
            _comandaProdutoService = comandaProdutoService;
            ComandaFinalizada = comandaFinalizada;
            Task.Run(GetComandaFinalizadaProdutosAsync);

        }

        [ObservableProperty]
        public ComandaModel? _comandaFinalizada;

        public ObservableCollection<ComandaProduto> ComandaFinalizadaProdutoAdicionados { get; set; } = new ObservableCollection<ComandaProduto>();
        public ObservableCollection<ProdutoModel> ProdutosNaComandaFinalizada { get; set; } = new ObservableCollection<ProdutoModel>();

        public ComandaFechadaViewModel()
        {
            
        }


        [RelayCommand]
        public async Task GetComandaFinalizadaProdutosAsync()
        {
            try
            {
                await _comandaService.InitializeAsync();
                await _comandaProdutoService.InitializeAsync();
                await _produtoService.InitializeAsync();

                var comandasProdutos = await _comandaProdutoService.GetComandaProdutos();

                ProdutosNaComandaFinalizada.Clear();
                ComandaFinalizadaProdutoAdicionados.Clear();

                foreach (var comandaProduto in comandasProdutos)
                {
                    if (comandaProduto.ComandaId == ComandaFinalizada.Id)
                    {
                        ComandaFinalizadaProdutoAdicionados.Add(comandaProduto);

                        var produto = await _produtoService.GetProdutoPorId(comandaProduto.ProdutoId);

                        if (produto != null)
                        {
                            ProdutosNaComandaFinalizada.Add(produto);

                            produto.QuantidadeDeProduto = comandaProduto.QuantidadeDeProduto;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter produtos de comanda: {ex.Message}");
            }
        }
    }
}

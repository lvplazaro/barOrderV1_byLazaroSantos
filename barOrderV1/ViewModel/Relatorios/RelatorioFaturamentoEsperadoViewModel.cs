using barOrderV1.Model;
using barOrderV1.Model.Auxiliar;
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

namespace barOrderV1.ViewModel.Relatorios
{
    public partial class RelatorioFaturamentoEsperadoViewModel : BaseViewModel
    {
        private readonly IProdutoService _produtoService;

        public RelatorioFaturamentoEsperadoViewModel(IProdutoService produtoService)
        {
            _produtoService = produtoService;
            Task.Run(GetProdutosAsync);

        }


        [ObservableProperty]
        public CategoriaProduto? _categoriaSelecionada;

        [ObservableProperty]
        public double? _totalDaCategoria;

        public ObservableCollection<ProdutoModel> Produtos { get; set; } = new ObservableCollection<ProdutoModel>();
        public ObservableCollection<ProdutoValorTotalEstoqueModel>? ProdutosFiltrados { get; set; } = new ObservableCollection<ProdutoValorTotalEstoqueModel>();

        public IList<CategoriaProduto> CategoriasList => Enum.GetValues(typeof(CategoriaProduto)).Cast<CategoriaProduto>().ToList();


        [RelayCommand]
        public async Task GetProdutosAsync()
        {

            try
            {
                await _produtoService.InitializeAsync();
                var produtos = await _produtoService.GetProdutos();

                Produtos.Clear();

                if (produtos.Any())
                {
                    foreach (var produto in produtos)
                    {
                        Produtos.Add(produto);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }


        [RelayCommand]
        public async Task CalcFatPrevisto()
        {
            try
            {
                var categoria = (CategoriaProduto)CategoriaSelecionada;
                
                var produtosFiltrados = Produtos.Where(produto => produto.Categoria == categoria);

                ProdutosFiltrados.Clear();

                double TotalDaCateg = 0.0;

                foreach (var produto in produtosFiltrados)
                {
                    double valorTotalEstoque = produto.Preco * produto.QuantidadeEstoque;

                    var produtoValorTotalEstoque = new ProdutoValorTotalEstoqueModel
                    {
                        Nome = produto.Nome,
                        Categoria = produto.Categoria,
                        Preco = produto.Preco,
                        QuantidadeEstoque = produto.QuantidadeEstoque,
                        ValorTotalEmEstoque = valorTotalEstoque
                    };

                    ProdutosFiltrados.Add(produtoValorTotalEstoque);
                    TotalDaCateg += valorTotalEstoque;
                }

                TotalDaCategoria = TotalDaCateg;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }



       

    




    }
}

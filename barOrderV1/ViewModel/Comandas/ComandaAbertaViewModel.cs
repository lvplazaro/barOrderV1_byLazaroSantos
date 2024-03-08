using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using barOrderV1.View.Comandas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.ViewModel.Comandas
{
    //[QueryProperty(nameof(ComandaEditavel), "ComandaObject")]
    public partial class ComandaAbertaViewModel : BaseViewModel
    {

        private readonly IComandaProdutoService? _comandaProdutoService;
        private readonly IProdutoService? _produtoService;
        private readonly IComandaService? _comandaService;


        [ObservableProperty]
        public ComandaModel? _comandaEditavel;

        [ObservableProperty]
        private ProdutoModel _incremento;

        public ComandaModel? ComandaModel { get; private set; }

        public ComandaAbertaViewModel(IComandaService comandaService, IProdutoService produtoService, IComandaProdutoService comandaProdutoService, ComandaModel comandaEditavel)
        {
            ComandaEditavel = comandaEditavel;
            _comandaProdutoService = comandaProdutoService;
            _comandaService = comandaService;
            _produtoService = produtoService;
            Task.Run(GetComandaProdutosAsync);
            Task.Run(AtualizarTotal);

        }

        public ComandaAbertaViewModel()
        {
            
        }

        public ObservableCollection<ComandaProduto> ComandaProdutoAdicionados { get; set; } = new ObservableCollection<ComandaProduto>();
        public ObservableCollection<ProdutoModel> ProdutosNaComanda { get; set; } = new ObservableCollection<ProdutoModel>();



        [RelayCommand]
        private async Task UpdateComanda()
        {
            if (ComandaEditavel == null)
            {
                await Shell.Current.DisplayAlert("Erro", "Nenhum produto selecionado para edição.", "Ok");
                return;
            }
            try
            {
                await _comandaService.InitializeAsync();
                await _comandaService.UpdateComanda(ComandaEditavel);

                await Shell.Current.DisplayAlert("Sucesso", "Comanda editada com sucesso!", "Ok");

                await Shell.Current.GoToAsync("../..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "ok");
            }
        }

        [RelayCommand]
        async Task IrParaProdutosPopUp(CategoriaProduto CatSelecionada)
        {
            ProdutoService produtoService = new ProdutoService();
            ComandaService comandaService = new ComandaService();
            ComandaProdutoService comandaProdutoService = new ComandaProdutoService(comandaService,produtoService);

            var produtosPopUpViewModel = new ProdutosPopUpViewModel(CatSelecionada, produtoService, ComandaEditavel,comandaProdutoService);
            var produtosPopView = new ProdutosPopUpView(produtosPopUpViewModel);
            await Shell.Current.Navigation.PushAsync(produtosPopView);
        }


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
                    if (comandaProduto.ComandaId == ComandaEditavel.Id)
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

                // Após iterar sobre todos os produtos, atualize o total da comanda baseado nos produtos
                //ComandaEditavel.Total = ProdutosNaComanda.Sum(p => p.Preco * p.QuantidadeDeProduto);
                await AtualizarTotal();
                await _comandaService.UpdateComanda(ComandaEditavel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter produtos de comanda: {ex.Message}");
            }
        }

        public async Task AtualizarTotal()
        {
            ComandaEditavel.Total = ProdutosNaComanda.Sum(p => p.Preco * p.QuantidadeDeProduto);

        }

        [RelayCommand]
        public async Task IncrementarQuantidadeProduto(ProdutoModel produto)
        {
            try
            {
                var result = await Shell.Current.DisplayAlert("Adicionar", $"Adicionar: \n\n \"{produto.Nome}\" ?", "Sim", "Não");

                if (result)
                {
                    var comandaId = Convert.ToInt32(ComandaEditavel.Id);
                    var produtoId = Convert.ToInt32(produto.Id);

                    var prod = await _produtoService.GetProdutoPorId(produtoId);

                    int quantidadeEstoque = prod.QuantidadeEstoque;
                    int quantidadeAdicionada = 0;

                    foreach (var comandaProduto in ComandaProdutoAdicionados)
                    {
                        if (comandaProduto.ComandaId == comandaId && comandaProduto.ProdutoId == produtoId)
                        {
                            quantidadeAdicionada = comandaProduto.QuantidadeDeProduto;
                            break;
                        }
                    }

                    int quantidadeSolicitada = 1; // Por enquanto, estamos adicionando apenas 1 unidade


                    if (prod.QuantidadeEstoque >= quantidadeSolicitada)
                    {
                        if (prod.QuantidadeEstoque == 1)
                        {
                            await Shell.Current.DisplayAlert("Aviso", $"Ultimo: \n\n \"{prod.Nome}\" em estoque !", "Ok");

                        }
                         else if (prod.QuantidadeEstoque <= prod.QuantidadeCritica + 1)
                        {
                            
                            await Shell.Current.DisplayAlert("Aviso", "Produto em quantidade critica!", "Ok");

                        }

                        await _comandaProdutoService.AdicionarQuantidadeProdutoAComanda(comandaId, produtoId, 1);
                        await GetComandaProdutosAsync();

                        // Atualizar quantidade em estoque do produto
                        prod.QuantidadeEstoque -= 1;
                        await _produtoService.UpdateProduto(prod);

                        MessagingCenter.Send(this, "ProdutoAtualizado");

                        await AtualizarTotal();

                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Aviso", "Quantidade excede o estoque disponível.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao incrementar quantidade de produto na ViewModel: {ex.Message}");
            }
        }



        [RelayCommand]
        public async Task DecrementarQuantidadeProduto(ProdutoModel produto)
        {
            try
            {
                var result = await Shell.Current.DisplayAlert("Remover", $"Remover: \n\n \"{produto.Nome}\" ?", "Sim", "Não");

                if (result)
                {
                    var comandaId = Convert.ToInt32(ComandaEditavel.Id);
                    var produtoId = Convert.ToInt32(produto.Id);

                    var prod = await _produtoService.GetProdutoPorId(produtoId);

                    int quantidadeEstoque = prod.QuantidadeEstoque;
                    int quantidadeAdicionada = 0;

                    foreach (var comandaProduto in ComandaProdutoAdicionados)
                    {
                        if (comandaProduto.ComandaId == comandaId && comandaProduto.ProdutoId == produtoId)
                        {
                            quantidadeAdicionada = comandaProduto.QuantidadeDeProduto;
                            break;
                        }
                    }

                    int quantidadeSolicitada = 1; // Por enquanto, estamos adicionando apenas 1 unidade


                    if (quantidadeAdicionada > 1)
                    {

                        await _comandaProdutoService.DiminuirQuantidadeProdutoAComanda(comandaId, produtoId, 1);
                        await GetComandaProdutosAsync();

                        // Atualizar quantidade em estoque do produto
                        prod.QuantidadeEstoque += 1;
                        await _produtoService.UpdateProduto(prod);

                        MessagingCenter.Send(this, "ProdutoAtualizado");

                        await AtualizarTotal();

                    }
                    else
                    {
                        // Remove o produto da comanda
                        await _comandaProdutoService.RemoverProdutoDeComanda(comandaId, produtoId);
                        await GetComandaProdutosAsync();

                        // Exclui a linha correspondente no banco de dados
                        // Agora, aqui você deve implementar o método para excluir a linha no banco de dados

                        // Atualiza quantidade em estoque do produto
                        prod.QuantidadeEstoque += 1;
                        await _produtoService.UpdateProduto(prod);

                        MessagingCenter.Send(this, "ProdutoAtualizado");

                        await AtualizarTotal(); 
                        await Shell.Current.DisplayAlert("Aviso", "Produto removido da comanda.", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao incrementar quantidade de produto na ViewModel: {ex.Message}");
            }
        }

    }
}


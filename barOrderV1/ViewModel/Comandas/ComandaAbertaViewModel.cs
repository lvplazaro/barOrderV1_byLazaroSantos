using barOrderV1.Model;
using barOrderV1.Model.Enums;
using barOrderV1.Services;
using barOrderV1.View.Comandas;
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
    //[QueryProperty(nameof(ComandaEditavel), "ComandaObject")]
    public partial class ComandaAbertaViewModel : BaseViewModel
    {

        private readonly IComandaProdutoService? _comandaProdutoService;
        private readonly IProdutoService? _produtoService;
        private readonly IComandaService? _comandaService;


        [ObservableProperty]
        private ComandaModel? _comandaEditavel;
        public ComandaModel? ComandaModel { get; private set; }

        public ComandaAbertaViewModel(IComandaService comandaService, IProdutoService produtoService, IComandaProdutoService comandaProdutoService, ComandaModel comandaEditavel)
        {
            ComandaEditavel = comandaEditavel;
            _comandaProdutoService = comandaProdutoService;
            _comandaService = comandaService;
            _produtoService = produtoService;
            Task.Run(GetComandaProdutosAsync);
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
            ComandaModel comandaAdicionar = ComandaEditavel;

            try
            {
                // Inicializa os serviços
                await _comandaProdutoService.InitializeAsync();
                await _produtoService.InitializeAsync();

                // Obtém os itens de comanda
                var comandasProdutos = await _comandaProdutoService.GetComandaProdutos();

                // Limpa a lista de produtos da comanda
                ProdutosNaComanda.Clear();

                // Adiciona os itens de comanda à lista
                // Adiciona os itens de comanda à lista
                foreach (var comandaProduto in comandasProdutos)
                {
                    if (comandaProduto.ComandaId == comandaAdicionar.Id)
                    {
                        // Adiciona o item de comanda à lista
                        ComandaProdutoAdicionados.Add(comandaProduto);

                        // Obtém o produto associado a este item de comanda
                        var produto = await _produtoService.GetProdutoPorId(comandaProduto.ProdutoId);

                        // Se o produto não for nulo, adiciona-o à lista de produtos da comanda
                        if (produto != null)
                        {
                            ProdutosNaComanda.Add(produto);
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


﻿using barOrderV1.Model;
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
    public partial class RelatorioEstoqueCriticoViewModel : BaseViewModel
    {

        private readonly IProdutoService _produtoService;

        public RelatorioEstoqueCriticoViewModel(IProdutoService produtoService)
        {
            _produtoService = produtoService;
            Task.Run(GetProdutosAsync);
        }

        [ObservableProperty]
        public CategoriaProduto? _categoriaSelecionada;

        public ObservableCollection<ProdutoModel> Produtos { get; set; } = new ObservableCollection<ProdutoModel>();

        public ObservableCollection<ProdutoModel>? ProdutosCriticos { get; set; } = new ObservableCollection<ProdutoModel>();

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
        public async Task CalcProdEstCritico()
        {
            try
            {
                var categoria = (CategoriaProduto)CategoriaSelecionada;

                // Filtra os produtos com base na categoria
                var produtosFiltrados = Produtos.Where(produto => produto.Categoria == categoria);

                // Limpa a lista de produtos filtrados
                ProdutosCriticos.Clear();

                // Adiciona os produtos que estão em estado crítico à lista de produtos filtrados
                foreach (var produto in produtosFiltrados.Where(p => p.QuantidadeEstoque < p.QuantidadeCritica))
                {
                    ProdutosCriticos.Add(produto);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }






    }
}

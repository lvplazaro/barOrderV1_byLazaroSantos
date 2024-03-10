using barOrderV1.Model;
using barOrderV1.Services;
using barOrderV1.View.Comandas;
using barOrderV1.ViewModel.Comandas;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.ViewModel
{
    public partial class ComandasFechadasViewModel : BaseViewModel
    {
        private readonly IComandaService _comandaService;
        private readonly IComandaProdutoService _comandaProdutoService;
        public ObservableCollection<ComandaModel> ComandasFechadas { get; set; } = new ObservableCollection<ComandaModel>();

        public ComandasFechadasViewModel(IComandaService comandaService, IComandaProdutoService comandaProdutoService)
        {
            _comandaService = comandaService;           
            _comandaProdutoService = comandaProdutoService;
            Task.Run(GetComandasFechadasAsync);
        }

        private string _nomePesquisado;
        public string NomePesquisado
        {
            get => _nomePesquisado;
            set
            {
                SetProperty(ref _nomePesquisado, value);
                GetComandasFechadasAsync();
            }
        }



        public async Task GetComandasFechadasAsync()
        {
            try
            {
                await _comandaService.InitializeAsync();
                var comandas = await _comandaService.GetComandas();

                ComandasFechadas.Clear();

                if (comandas.Any())
                {
                    foreach (var comanda in comandas.Where(c => c.Status == Model.Enums.ComandaStatus.Fechada).OrderByDescending(c => c.DataFechamento))
                    {
                        ComandasFechadas.Add(comanda);
                    }

                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }


        [RelayCommand]
        Task IrParaComandaFechada(ComandaModel comandaFinalizada)
        {
            ProdutoService produtoService = new ProdutoService();
            ComandaService comandaService = new ComandaService();
            ComandaProdutoService comandaProdutoService = new ComandaProdutoService(comandaService, produtoService);

            var comandaFechadaViewModel = new ComandaFechadaViewModel(comandaService, produtoService, comandaProdutoService, comandaFinalizada);
            var comandaFechadaView = new ComandaFechadaView(comandaFechadaViewModel);
            return Shell.Current.Navigation.PushAsync(comandaFechadaView);

        }

        [RelayCommand]
        public async Task DeletarComanda(ComandaModel comanda)
        {
            var confirmacao = await Shell.Current.DisplayAlert("Deletar", $"Confirma exclusão da comanda: \n\n \"{comanda.Nome}\" ?", "Sim", "Não");

            if (confirmacao)
            {
                try
                {
                    await _comandaService.InitializeAsync();
                    await _comandaProdutoService.InitializeAsync();

                    
                    await _comandaProdutoService.DeletarComanda(comanda.Id);
                    

                    await _comandaService.DeleteComanda(comanda);

                    MessagingCenter.Send(this, "ProdutoAtualizado");

                    await Shell.Current.DisplayAlert("Sucesso", "Comanda deletada com sucesso!", "Ok");

                    await GetComandasFechadasAsync();
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro", ex.Message, "Ok");
                }
            }
        }

        [RelayCommand]
        public async Task SearchBarPaginaInicial(string NomePesquisado)
        {
            try
            {
                if (!string.IsNullOrEmpty(NomePesquisado))
                {
                    var foundComandas = ComandasFechadas.Where(found => found.Nome.Contains(NomePesquisado)).ToList();
                    if (foundComandas.Count > 0)
                    {
                        ComandasFechadas.Clear();
                        foreach (var comanda in foundComandas)
                        {
                            ComandasFechadas.Add(comanda);
                        }
                    }


                }


                else
                {
                    await GetComandasFechadasAsync();
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        }


    }
}

﻿using barOrderV1.View;
using barOrderV1.View.Comandas;

namespace barOrderV1
{
    public partial class AppShell : Shell
    {
        public AppShell(ServiceProvider serviceProvider)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EditProdutoView), typeof(EditProdutoView));
            Routing.RegisterRoute(nameof(EstoqueDetailsView), typeof(EstoqueDetailsView));
            Routing.RegisterRoute(nameof(AddProdutoView), typeof(AddProdutoView));

            Routing.RegisterRoute(nameof(ProdutosPopUpView), typeof(ProdutosPopUpView));
            Routing.RegisterRoute(nameof(ComandaAbertaView), typeof(ComandaAbertaView));
            Routing.RegisterRoute(nameof(FechamentoDeComandaView), typeof(FechamentoDeComandaView));
            Routing.RegisterRoute(nameof(ComandasFechadasView), typeof(ComandasFechadasView));




        }
    }
}

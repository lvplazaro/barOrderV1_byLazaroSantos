using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Model.Enums
{
    public enum CategoriaProduto : int
    {
        
        CervejaLata = 0,
        CervejaGarrafa = 1,
        BebidasDose = 2,
        Refrigerantes = 3,
        Vinho = 4,
        LitroDestilado = 5,
        Suco = 6,
        Outros = 7,
    }

    public static class CategoriaProdutoExtensions
    {
        public static string GetAlias(this CategoriaProduto cat)
        {
            switch (cat)
            {
                case CategoriaProduto.CervejaLata:
                    return "Cervejas Lata";
                case CategoriaProduto.CervejaGarrafa:
                    return "Cervejas Garrafa";
                case CategoriaProduto.BebidasDose:
                    return "Bebidas em Dose";
                case CategoriaProduto.Refrigerantes:
                    return "Refrigerantes";
                case CategoriaProduto.Vinho:
                    return "Vinhos";
                case CategoriaProduto.LitroDestilado:
                    return "Litros Destilado";
                case CategoriaProduto.Suco:
                    return "Sucos";
                case CategoriaProduto.Outros:
                    return "Outros";
                default:
                    throw new ArgumentException("Categoria Inválida.");
            }
        }
    }
}

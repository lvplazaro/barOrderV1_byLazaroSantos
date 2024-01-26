using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Model.Enums
{
    internal enum FormaPagamento : int
    {
        Dinheiro = 0,
        CartaoCredito = 1,
        CartaoDebito = 2,
        Pix = 3,
        Outros = 4,
    }
}

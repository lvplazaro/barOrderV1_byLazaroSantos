using barOrderV1.Model.Enums;
using System.Globalization;


namespace barOrderV1.Services
{
    public class ConverterAliasParaEnumCategoriaProduto : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string categoriaString)
            {
                if (Enum.TryParse(typeof(CategoriaProduto), categoriaString, out object categoriaProduto))
                {
                    return ((CategoriaProduto)categoriaProduto).GetAlias();
                }
            }
            return string.Empty;
        }


        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            
            throw new NotImplementedException();
        }
    }
}

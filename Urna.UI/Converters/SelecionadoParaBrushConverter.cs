using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Urna.UI.Converters;

public class SelecionadoParaBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool selecionado = (bool)value!;
        selecionado = (string)parameter! == "texto" ? !selecionado: selecionado;
        return selecionado ? Brushes.Black : Brushes.White;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
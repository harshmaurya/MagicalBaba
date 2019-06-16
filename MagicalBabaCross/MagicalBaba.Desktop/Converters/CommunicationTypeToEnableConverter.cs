using System;
using System.Globalization;
using System.Windows.Data;
using MagicalBaba.BaseLibrary.DomainModel;

namespace MagicalBaba.Desktop.Converters
{
    class CommunicationTypeToEnableConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (CommunicationType) value != CommunicationType.GetAnswer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using ShopWPF_EF.MyCollection;
using TabelsDB;

namespace ShopWPF_EF.ViewModel
{
    class ConverterStatistics : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return String.Format("Количество элементов - {0}", (int)value - 1);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConverterToolTip : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return String.IsNullOrEmpty((string) value) ? "Описание для этого товара отсутствует" : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterBinding<T1,T2> : IMultiValueConverter where T1:class
    {
        private readonly ElementCollection<T1,T2> _elementCollection = new ElementCollection<T1,T2>();

        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            foreach (var item in values)
            {
                if (item is T1) _elementCollection.FirstItem = (T1) item;
                else if (item is T2) _elementCollection.SecondItem = (T2) item;
            }
            return _elementCollection;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    class ElementCollection<T1,T2> where T1: class 
    {
        public T1 FirstItem { get; set; }
        public T2 SecondItem { get; set; }
    }
    
}
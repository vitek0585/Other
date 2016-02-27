using System.Security;
using System.Windows;
using System.Windows.Controls;
using ShopWPF_EF.ViewModel;

namespace ShopWPF_EF.Converters
{
    public class ConverterBindingWrapper : ConverterBinding<ItemCollection,string>
    {

    }
    public class ConverterForEnterWrapper : ConverterBinding<Window,PasswordBox>
    {

    }
}
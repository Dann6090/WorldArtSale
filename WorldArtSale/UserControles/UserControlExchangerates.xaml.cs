using BIZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorldArtSale
{
    /// <summary>
    /// Interaction logic for UserControlExchangerates.xaml
    /// </summary>
    public partial class UserControlExchangerates : UserControl
    {
        ClassBIZ BIZ;
        public UserControlExchangerates(ClassBIZ inBIZ)
        {
            InitializeComponent();
            BIZ = inBIZ;
            MainGrid.DataContext = BIZ.CBC;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                await BIZ.UpdateRates();
                await Task.Delay(300000);
            }
        }
    }
}

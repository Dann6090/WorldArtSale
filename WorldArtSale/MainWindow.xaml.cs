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
using WorldArtSale.UserControles;

namespace WorldArtSale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassBIZ BIZ;        
        UserControlAuctionItem UCAI;
        UserControlExchangerates UCEr;        
        UserControlCustomerView UCCV;
        UserControlBidCalculation UCBC;

        public MainWindow()
        {
            InitializeComponent();
            BIZ = new ClassBIZ();
            MainGrid.DataContext = BIZ;
            
            UCAI = new UserControlAuctionItem(BIZ, GridTopMiddel);
            UCEr = new UserControlExchangerates(BIZ);           
            UCCV = new UserControlCustomerView(BIZ, GridTopLeft);
            UCBC = new UserControlBidCalculation(BIZ);

            GridTopLeft.Children.Add(UCCV);
            GridTopMiddel.Children.Add(UCAI);
            GridTopRight.Children.Add(UCBC);
            GridBottom.Children.Add(UCEr);
        }
    }
}

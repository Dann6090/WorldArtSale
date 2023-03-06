using BIZ;
using Repository;
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
using WorldArtSale;

namespace WorldArtSale
{
    /// <summary>
    /// Interaction logic for UserControlAuctionItem.xaml
    /// </summary>
    public partial class UserControlAuctionItem : UserControl
    {
        ClassBIZ BIZ;
        Grid homeGrid;
        public UserControlAuctionItem(ClassBIZ inBiz, Grid inGrid)
        {
            InitializeComponent();
            BIZ = inBiz;
            MainGrid.DataContext = BIZ;
            homeGrid = inGrid;
        }

        private void ButtonAddNewArt_Click(object sender, RoutedEventArgs e)
        {
            BIZ.fallbackArt = new ClassArt();
            BIZ.GridTopLeftEnabled = false;
            BIZ.GridTopRightEnabled= false;
            UserControlAuctionItemEdit UCAIE = new UserControlAuctionItemEdit(BIZ, homeGrid);
            homeGrid.Children.Add(UCAIE);
        }

        private void ButtonEditArt_Click(object sender, RoutedEventArgs e)
        {
            if (BIZ.selectedArt != null && BIZ.selectedArt.id > 0)
            {
                BIZ.fallbackArt = new ClassArt(BIZ.selectedArt);
                BIZ.GridTopLeftEnabled = false;
                BIZ.GridTopRightEnabled = false;
                UserControlAuctionItemEdit UCAIE = new UserControlAuctionItemEdit(BIZ, homeGrid);
                homeGrid.Children.Add(UCAIE); 
            }
            else
            {
                MessageBox.Show("Der er IKKE valgt noget kunstværk \nder kan redigeres !!!!", "ERROR !!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

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
using WorldArtSale.UserControles;

namespace WorldArtSale
{
    /// <summary>
    /// Interaction logic for UserControlCostumer.xaml
    /// </summary>
    public partial class UserControlCustomerView : UserControl
    {
        ClassBIZ BIZ;
        Grid homeGrid;
        public UserControlCustomerView(ClassBIZ inBiz, Grid inGrid)
        {
            InitializeComponent();
            BIZ = inBiz;
            MainGrid.DataContext = BIZ;
            homeGrid = inGrid;
            
        }
        private void ButtonNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            BIZ.GridTopMiddelEnabled = false;
            BIZ.GridTopRightEnabled = false;
            BIZ.fallbackCustomer = new ClassCustomer();
            UserControlCustomerEdit UCCE = new UserControlCustomerEdit(BIZ, homeGrid);
            homeGrid.Children.Add(UCCE);
        }

        private void ButtonEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (BIZ.selectedCustomer != null && BIZ.selectedCustomer.id > 0)
            {
                BIZ.GridTopMiddelEnabled = false;
                BIZ.GridTopRightEnabled = false;
                BIZ.fallbackCustomer = new ClassCustomer(BIZ.selectedCustomer);
                UserControlCustomerEdit UCCE = new UserControlCustomerEdit(BIZ, homeGrid);
                homeGrid.Children.Add(UCCE);
            }
        }
    }
}

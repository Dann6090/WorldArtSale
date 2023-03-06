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

namespace WorldArtSale.UserControles
{
    /// <summary>
    /// Interaction logic for UserControlCustomerEdit.xaml
    /// </summary>
    public partial class UserControlCustomerEdit : UserControl
    {
        ClassBIZ BIZ;
        Grid homeGrid;
        public UserControlCustomerEdit(ClassBIZ inBiz, Grid inGrid)
        {
            InitializeComponent();
            BIZ = inBiz;
            MainGrid.DataContext = BIZ;
            homeGrid = inGrid;
        }

        private void ButtonSaveData_Click(object sender, RoutedEventArgs e)
        {
            if (BIZ.fallbackCustomer.id > 0)
            {
                if (BIZ.UpdateCustomerDB() > 0)
                {
                    BIZ.GridTopMiddelEnabled = true;
                    BIZ.GridTopRightEnabled = true;
                    BIZ.listCustomer = BIZ.GetAllCustomerFromDbToList();
                    if (BIZ.fallbackCustomer.isActive)
                    {
                        BIZ.selectedCustomer = BIZ.listCustomer.Where(c => c.id == (BIZ.fallbackCustomer.id)).First();
                    }
                    BIZ.fallbackCustomer = new ClassCustomer();
                    homeGrid.Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Opdateringen i DB mislykkedes !!!!", "DB ERROR !!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                int newID = BIZ.InsertNewCustomerInDB();
                if (newID > 0)
                {
                    BIZ.GridTopMiddelEnabled = true;
                    BIZ.GridTopRightEnabled = true;
                    BIZ.listCustomer = BIZ.GetAllCustomerFromDbToList();
                    BIZ.selectedCustomer = BIZ.listCustomer.Where(c => c.id == (newID)).First();
                    BIZ.fallbackCustomer = new ClassCustomer();
                    homeGrid.Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Opdateringen i DB mislykkedes !!!!", "DB ERROR !!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonRegret_Click(object sender, RoutedEventArgs e)
        {
            BIZ.GridTopMiddelEnabled = true;
            BIZ.GridTopRightEnabled = true;
            BIZ.fallbackCustomer = new ClassCustomer();
            homeGrid.Children.Remove(this);
        }
    }
}

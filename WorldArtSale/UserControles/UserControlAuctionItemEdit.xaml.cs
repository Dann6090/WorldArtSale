using BIZ;
using Microsoft.Win32;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for UserControlAuctionItemEdit.xaml
    /// </summary>
    public partial class UserControlAuctionItemEdit : UserControl
    {
        ClassBIZ BIZ;
        Grid homeGrid;
        public UserControlAuctionItemEdit(ClassBIZ inBiz, Grid inGrid)
        {
            InitializeComponent();
            BIZ = inBiz;
            MainGrid.DataContext = BIZ;
            homeGrid = inGrid;
        }
        private void ButtonSaveNewArt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BIZ.fallbackArt.picturePath.Length > 0 && BIZ.fallbackArt.description.Length > 0 && BIZ.fallbackArt.titel.Length > 0)
                {
                    BIZ.GridTopLeftEnabled = true;
                    BIZ.GridTopRightEnabled = true;
                    BIZ.AddOrUpdateArtDB();
                    homeGrid.Children.Remove(this);
                }
                else
                {
                    MessageBox.Show("Der mangler data !!!!", "DATA TJEK !!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl !!!! \n\n{ex.Message}", "ERROR !!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            BIZ.GridTopLeftEnabled = true;
            if (BIZ.selectedArt.id >0)
            {
                BIZ.GridTopRightEnabled=true;
            }
            homeGrid.Children.Remove(this);            
        }
        private void ButtonSelectArt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.Filter = "JPG file (*.jpg)|*.jpg|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif|All files (*.*)|*.*";
                OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                // VIRKELIG GODT!!!
                if (OFD.ShowDialog() == true)
                {
                    string newPath = @"C:\ImageSale\" + OFD.SafeFileName;
                    if (!File.Exists(newPath))
                    {
                        File.Copy(OFD.FileName, newPath);
                    }
                    BIZ.fallbackArt.picturePath = newPath;
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
    }
}

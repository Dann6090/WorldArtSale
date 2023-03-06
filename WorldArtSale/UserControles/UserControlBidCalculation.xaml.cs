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

namespace WorldArtSale
{
    /// <summary>
    /// Interaction logic for UserControlBidCalculation.xaml
    /// </summary>
    public partial class UserControlBidCalculation : UserControl
    {
        ClassBidCalck CBC;
        ClassBIZ BIZ;
        public UserControlBidCalculation(ClassBIZ inBiz)
        {
            InitializeComponent();
            CBC = inBiz.CBC;
            BIZ = inBiz;
            MainGrid.DataContext = CBC;
        }

        private void ButtonRaiseBid_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxBidRaise.Text.Length != 0)
            {
                CBC.bidUSD += Convert.ToDouble(ComboBoxBidRaise.Text);
            }
        }

        private void RegisterPurchase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bekræft køb af kunstværk.\n\nTast Ja for Køb.\nTast Nej for at Annullere.",
                "Afslut budrunde", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    BIZ.InsertPurchasedArtDB();
                    CBC.bidUSD = 0D;
                    ComboBoxBidRaise.SelectedIndex = 0;
                    BIZ.GridTopRightEnabled = false;
                    BIZ.listArt = BIZ.GetAllArtFromDbToList();
                    BIZ.selectedArt = new ClassArt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl ved\nregistering af salg !!! \n\n{ex.Message}", "ERROR !!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitWithoutPurchase_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bekræft afslutning af bud.\n\nTast Ja for afbrydelse.\nTast Nej for at fortsætte.",
                "Afslut budrunde", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MessageBox.Show("Skal kunstværket\nfjernes fra oversigten?\n\nTast Ja for fjenelse.\nTast Nej for at bibeholde.",
                        "Afslut budrunde", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MessageBox.Show("Bekræft fjernes kunstværket.\n\nTast Ja for fjenelse.\nTast Nej for at bibeholde.",
                    "Afslut budrunde", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        // Indsæt kode der gør kunstværket inaktivt
                        if (BIZ.DeactivateArtDB(BIZ.selectedArt.id) > 0)
                        {
                            MessageBox.Show("Kunstværket er nu fjernet\nfra oversigten.", "Fjern kunstværk", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Opdateringen i DB mislykkedes !!!!", "DB ERROR !!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        // Nulstiller uc-bid og uc-art
                        CBC.bidUSD = 0D;
                        ComboBoxBidRaise.SelectedIndex= 0;
                        BIZ.GridTopRightEnabled = false;
                        BIZ.listArt = BIZ.GetAllArtFromDbToList();
                        BIZ.selectedArt = new ClassArt();
                    }
                    else
                    {
                        // Nulstiller uc-bid og uc-art
                        CBC.bidUSD = 0D;
                        ComboBoxBidRaise.SelectedIndex = 0;
                        BIZ.GridTopRightEnabled = false;
                        BIZ.listArt = BIZ.GetAllArtFromDbToList();
                        BIZ.selectedArt = new ClassArt();
                    }
            }
                else
                {
                    // Nulstiller uc-bid og uc-art
                    CBC.bidUSD = 0D;
                    ComboBoxBidRaise.SelectedIndex = 0;
                    BIZ.GridTopRightEnabled = false;
                    BIZ.listArt = BIZ.GetAllArtFromDbToList();
                    BIZ.selectedArt = new ClassArt();
                } 
            }
        }
    }
}

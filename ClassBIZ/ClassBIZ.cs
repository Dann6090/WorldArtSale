using IO;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIZ
{
    public class ClassBIZ : ClassNotify
    {
        ClassExchangeratesApi CERA = new ClassExchangeratesApi();
        ClassWorldArtsaleDB CWASDB = new ClassWorldArtsaleDB();
        //Feilds
        private ClassBidCalck _CBC;
        private ClassCustomer _selectedCustomer;
        private ClassCustomer _fallbackCustomer; 
        private List<ClassCustomer> _listCustomer; 
        private ClassCountry _selectedCountry; 
        private List<ClassCountry> _listCountry;
        private List<ClassCountry> _listCurrency;
        private ClassArt _selectedArt;
        private ClassArt _fallbackArt;
        private List<ClassArt> _listArt;

        private bool _GridTopRightEnabled;
        private bool _GridTopMiddelEnabled;
        private bool _GridTopLeftEnabled;
        //Consturtor
        public ClassBIZ()
        {
            //Initialaize
            CBC = new ClassBidCalck();
            selectedCustomer = new ClassCustomer();
            fallbackCustomer = new ClassCustomer();
            listCustomer = GetAllCustomerFromDbToList();
            selectedCountry = new ClassCountry();
            listCountry = GetAllCountryFromDbToList();
            listCurrency = new List<ClassCountry>(listCountry);
            selectedArt = new ClassArt();
            fallbackArt = new ClassArt();
            listArt = GetAllArtFromDbToList();

            GridTopLeftEnabled = true;
            GridTopMiddelEnabled = false;
            GridTopRightEnabled = false;
        }
        //properties
        public ClassCustomer selectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    if (_selectedCustomer.id > 0)
                    {
                        GridTopMiddelEnabled = true;
                        CBC.ownValutaCode = value.preferedCurrency.countryCode;
                    }
                    else
                    {
                        selectedArt = new ClassArt();
                        GridTopMiddelEnabled = false;
                        GridTopRightEnabled = false;
                    }
                }
                Notify("selectedCustomer");
            }
        }
        public ClassCustomer fallbackCustomer
        {
            get { return _fallbackCustomer; }
            set
            {
                if (_fallbackCustomer != value)
                {
                    _fallbackCustomer = value;
                }
                Notify("fallbackCustomer");
            }
        }
        public List<ClassCustomer> listCustomer
        {
            get { return _listCustomer; }
            set
            {
                if (_listCustomer != value)
                {
                    _listCustomer = value;
                }
                Notify("listCustomer");
            }
        }
        public ClassCountry selectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    if (selectedCustomer != null && selectedCustomer != null)
                    {
                        fallbackCustomer.country = _selectedCountry.countryName;
                    }
                }
                Notify("selectedCountry");
            }
        }
        public List<ClassCountry> listCountry
        {
            get { return _listCountry; }
            set
            {
                if (_listCountry != value)
                {
                    _listCountry = value;
                }
                Notify("listCountry");
            }
        }
        public List<ClassCountry> listCurrency
        {
            get { return _listCurrency.GroupBy(x => x.valutaName).Select(g => g.First()).OrderBy(d => d.valutaName).ToList();}
            set
            {
                if (_listCurrency != value)
                {
                    _listCurrency = value;
                }
                Notify("listCurrency");
            }
        }
        public ClassArt selectedArt
        {
            get { return _selectedArt; }
            set
            {
                if (_selectedArt != value)
                {
                    _selectedArt = value;
                    if (_selectedArt != null && _selectedArt.id > 0)
                    {
                        GridTopRightEnabled = true;
                    }
                    else
                    {
                        GridTopRightEnabled = false;
                    }
                }
                Notify("selectedArt");
            }
        }
        public ClassArt fallbackArt
        {
            get { return _fallbackArt; }
            set
            {
                if (_fallbackArt != value)
                {
                    _fallbackArt = value;
                }
                Notify("fallbackArt");
            }
        }
        public List<ClassArt> listArt
        {
            get { return _listArt; }
            set
            {
                if (_listArt != value)
                {
                    _listArt = value;
                }
                Notify("listArt");
            }
        }
        public bool GridTopRightEnabled
        {
            get { return _GridTopRightEnabled; }
            set
            {
                if (_GridTopRightEnabled != value)
                {
                    _GridTopRightEnabled = value;
                }
                Notify("GridTopRightEnabled");
            }
        }
        public bool GridTopMiddelEnabled
        {
            get { return _GridTopMiddelEnabled; }
            set
            {
                if (_GridTopMiddelEnabled != value)
                {
                    _GridTopMiddelEnabled = value;
                }
                Notify("GridTopMiddelEnabled");
            }
        }
        public bool GridTopLeftEnabled
        {
            get { return _GridTopLeftEnabled; }
            set
            {
                if (_GridTopLeftEnabled != value)
                {
                    _GridTopLeftEnabled = value;
                }
                Notify("GridTopLeftEnabled");
            }
        }
        public ClassBidCalck CBC
        {
            get { return _CBC; }
            set
            {
                if (_CBC != value)
                {
                    _CBC = value;
                }
            }
        }

        public async Task UpdateRates()
        {
            CBC.valutaRates = await CERA.GetDataFromOpenExchangeRatesAsync();
        }
        public List<ClassCountry> GetAllCountryFromDbToList()
        {
            if (listCountry != null)
            {
                listCountry.Clear();
            }
            return CWASDB.GetAllCountriesFromDB();
        }
        public List<ClassArt> GetAllArtFromDbToList()
        {
            if (listArt != null)
            {
                listArt.Clear();
            }
            return CWASDB.GetAllArtFromDB();
        }
        //method
        public void AddOrUpdateArtDB()
        {
            try
            {
                if (fallbackArt.id > 0)
                {
                    CWASDB.UpdateArtInDB(fallbackArt);
                    listArt.Clear();
                    listArt = GetAllArtFromDbToList();
                    if (fallbackArt.isActive)
                    {
                        selectedArt = listArt.Where(c => c.id == fallbackArt.id).First();
                    }
                    else
                    {
                        selectedArt = new ClassArt();
                    }
                    fallbackArt = new ClassArt();
                }
                else
                {
                    int newID = CWASDB.InsertArtInDB(fallbackArt);
                    listArt.Clear();
                    listArt = GetAllArtFromDbToList();
                    if (fallbackArt.isActive)
                    {
                        selectedArt = listArt.Where(c => c.id == newID).First();
                    }
                    else
                    {
                        selectedArt = new ClassArt();
                    }
                    fallbackArt = new ClassArt();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //class
        public int DeactivateArtDB(int inId)
        {
           return CWASDB.DeactivateArtInDB(inId);
        }
        public List<ClassCustomer> GetAllCustomerFromDbToList()
        {
            if (listCustomer != null)
            {
                listCustomer.Clear();
            }
            return CWASDB.GetAllCustomersFromDB();
        }
        public int UpdateCustomerDB()
        {
            return CWASDB.UpdateCustomerDataInDB(fallbackCustomer);
        }
        public int InsertNewCustomerInDB()
        {
            return CWASDB.InsertCustomerInDB(fallbackCustomer);
        }
        public int InsertPurchasedArtDB()
        {
            return CWASDB.BuyArt(selectedCustomer, selectedArt, CBC);
        }
    }
}
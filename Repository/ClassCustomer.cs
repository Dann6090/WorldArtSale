using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassCustomer : ClassNotify
    {
        private int _id;
        private string _name;
        private string _address; 
        private string _zipCity;
        private string _country;
        private string _email;
        private string _phoneNo;
        private double _maxBid; 
        private ClassCountry _preferedCurrency;
        private bool _isActive;
        public ClassCustomer()
        {
            id = 0;
            name = string.Empty;
            address = string.Empty; 
            zipCity = string.Empty;
            country = string.Empty;
            email = string.Empty;
            phoneNo = string.Empty;
            maxBid = 0D;
            preferedCurrency = new ClassCountry();
            isActive = false;
        }
        public ClassCustomer(ClassCustomer inCustomer)
        {
            id = inCustomer._id;
            name = inCustomer.name;
            address = inCustomer.address;
            zipCity = inCustomer.zipCity;
            country = inCustomer.country;
            email = inCustomer.email;
            phoneNo = inCustomer.phoneNo;
            maxBid = inCustomer.maxBid;
            preferedCurrency = new ClassCountry(inCustomer.preferedCurrency);
            isActive = inCustomer.isActive;
        }

        public int id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
                Notify("id");
            }
        }        
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                }
                Notify("name");
            }
        }
        public string address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                }
                Notify("address");
            }
        }
        public string zipCity
        {
            get { return _zipCity; }
            set
            {
                if (_zipCity != value)
                {
                    _zipCity = value;
                }
                Notify("zipCity");
            }
        }
        public string country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                }
                Notify("country");
            }
        }
        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                }
                Notify("email");
            }
        }
        public string phoneNo
        {
            get { return _phoneNo; }
            set
            {
                if (_phoneNo != value)
                {
                    _phoneNo = value;
                }
                Notify("phoneNo");
            }
        }
        public double maxBid
        {
            get { return _maxBid; }
            set
            {
                if (_maxBid != value)
                {
                    _maxBid = value;
                }
                Notify("maxBid");
            }
        }
        public ClassCountry preferedCurrency
        {
            get { return _preferedCurrency; }
            set
            {
                if (_preferedCurrency != value)
                {
                    _preferedCurrency = value;
                }
                Notify("preferedCurrency");
            }
        }
        public bool isActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                }
                Notify("isActive");
            }
        }

    }
}

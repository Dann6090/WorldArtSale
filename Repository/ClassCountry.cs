using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassCountry : ClassNotify
    {
        private int _id;
        private string _countryCode;
        private string _countryName; 
        private string _valutaName;
        public ClassCountry()
        {
            id= 0;
            countryCode = string.Empty;
            countryName = string.Empty;
            valutaName = string.Empty;
        }
        public ClassCountry(ClassCountry inCountry)
        {
            id = inCountry.id;
            countryCode = inCountry.countryCode;
            countryName = inCountry.countryName;
            valutaName = inCountry.valutaName;
        }
        public ClassCountry(string inCode, string inName, string inValutaName)
        {
            id = 0;
            countryCode = inCode;
            countryName = inName;
            valutaName = inValutaName;
        }
        public ClassCountry(int inId, string inCode, string inName, string inValutaName)
        {
            id = inId;
            countryCode = inCode;
            countryName = inName;
            valutaName = inValutaName;
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
        public string countryCode
        {
            get { return _countryCode; }
            set
            {
                if (_countryCode != value)
                {
                    _countryCode = value;
                }
                Notify("countryCode");
            }
        }
        public string countryName
        {
            get { return _countryName; }
            set
            {
                if (_countryName != value)
                {
                    _countryName = value;
                }
                Notify("countryName");
            }
        }        
        public string valutaName
        {
            get { return _valutaName; }
            set
            {
                if (_valutaName != value)
                {
                    _valutaName = value;
                }
                Notify("valutaName");
            }
        }

    }
}

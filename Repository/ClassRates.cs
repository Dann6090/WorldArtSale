using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassRates : ClassNotify
    {
        private string _myBase;
        private int _timestamp;
        private string _disclaimer;
        private string _license;
        private Dictionary<string, double> _rates;
        private Dictionary<string, ClassCountryRate> _ratesFull;

        public ClassRates()
        {
            myBase = string.Empty;
            timestamp = 0;
            disclaimer = string.Empty;
            license = string.Empty;
            rates = new Dictionary<string, double>();
            ratesFull = new Dictionary<string, ClassCountryRate>();
        }

        [JsonProperty(PropertyName = "base")]
        public string myBase
        {
            get { return _myBase; }
            set
            {
                if (_myBase != value)
                {
                    _myBase = value;
                }
                Notify("myBase");
            }
        }
        public int timestamp
        {
            get { return _timestamp; }
            set
            {
                if (_timestamp != value)
                {
                    _timestamp = value;
                }
                Notify("timestamp");
            }
        }
        public string disclaimer
        {
            get { return _disclaimer; }
            set
            {
                if (_disclaimer != value)
                {
                    _disclaimer = value;
                }
                Notify("disclaimer");
            }
        }
        public string license
        {
            get { return _license; }
            set
            {
                if (_license != value)
                {
                    _license = value;
                }
                Notify("license");
            }
        }
        public Dictionary<string, double> rates
        {
            get { return _rates; }
            set
            {
                if (_rates != value)
                {
                    _rates = value;
                }
                Notify("rates");
            }
        }
        public Dictionary<string, ClassCountryRate> ratesFull
        {
            get { return _ratesFull; }
            set
            {
                if (_ratesFull != value)
                {
                    _ratesFull = value;
                }
                Notify("ratesFull");
            }
        }

        public void CalculateCountryRatesFull()
        {
            double krKurs = rates["DKK"];
            ratesFull.Clear();
            foreach (var rate in rates)
            {
                ratesFull.Add(rate.Key, new ClassCountryRate(rate.Key, rate.Value, krKurs));
            }
        }        
    }

    public class ClassCountryRate : ClassNotify
    {
        private string _countryKey;
        private double _dubRate;
        private string _strRate;

        public ClassCountryRate()
        {
            countryKey = string.Empty;
            dubRate = 0D;
            strRate = string.Empty;
        }

        public ClassCountryRate(string inKey, double inRate, double inDKRate)
        {
            countryKey = inKey;
            dubRate = inRate;
            strRate = CalculateDKRates(inRate, inDKRate);
        }

        public string countryKey
        {
            get { return _countryKey; }
            set
            {
                if (_countryKey != value)
                {
                    _countryKey = value;
                }
                Notify("countryKey");
            }
        }
        public double dubRate
        {
            get { return _dubRate; }
            set
            {
                if (_dubRate != value)
                {
                    _dubRate = value;
                }
                Notify("dubRate");
            }
        }
        public string strRate
        {
            get { return _strRate; }
            set
            {
                if (_strRate != value)
                {
                    _strRate = value;
                }
                Notify("strRate");
            }
        }
        private string CalculateDKRates(double countryRate, double inDKRate)
        {
            return ((1 / countryRate) * inDKRate).ToString("N4");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassBidCalck : ClassNotify
    {
        private string _ownValutaCode;
        private ClassRates _valutaRates;
        private double _bidUSD;
        private double _bidEUR;
        private double _bidOwnValuta;
        private double _bidWithFeeUSD;
        private double _bidWithFeeEUR;
        private double _bidWithFeeOwnValuta;
        public ClassBidCalck()
        {
            ownValutaCode = string.Empty;
            valutaRates = new ClassRates();
            bidUSD = 0D;
            bidEUR = 0D;
            bidOwnValuta = 0D;
            bidWithFeeUSD = 0D;
            bidWithFeeEUR = 0D;
            bidWithFeeOwnValuta = 0D;

        }
        public string ownValutaCode
        {
            get { return _ownValutaCode; }
            set
            {
                if (_ownValutaCode != value)
                {
                    _ownValutaCode = value;
                }
                Notify("ownValutaCode");
            }
        }
        public ClassRates valutaRates
        {
            get { return _valutaRates; }
            set
            {
                if (_valutaRates != value)
                {
                    _valutaRates = value;
                    if (_valutaRates.rates.Count > 0)
                    {
                        _valutaRates.CalculateCountryRatesFull();
                    }
                }
                Notify("valutaRates");
            }
        }
        public double bidUSD
        {
            get { return _bidUSD; }
            set
            {
                if (_bidUSD != value)
                {
                    if (double.TryParse(value.ToString(), out double X))
                    {
                        _bidUSD = X;
                        CalculateAllValues();
                    }
                }
                Notify("bidUSD");
            }
        }
        public double bidEUR
        {
            get { return _bidEUR; }
            set
            {
                if (_bidEUR != value)
                {
                    _bidEUR = value;
                }
                Notify("bidEUR");
            }
        }
        public double bidOwnValuta
        {
            get { return _bidOwnValuta; }
            set
            {
                if (_bidOwnValuta != value)
                {
                    _bidOwnValuta = value;
                }
                Notify("bidOwnValuta");
            }
        }
        public double bidWithFeeUSD
        {
            get { return _bidWithFeeUSD; }
            set
            {
                if (_bidWithFeeUSD != value)
                {
                    _bidWithFeeUSD = value;
                }
                Notify("bidWithFeeUSD");
            }
        }
        public double bidWithFeeEUR
        {
            get { return _bidWithFeeEUR; }
            set
            {
                if (_bidWithFeeEUR != value)
                {
                    _bidWithFeeEUR = value;
                }
                Notify("bidWithFeeEUR");
            }
        }
        public double bidWithFeeOwnValuta
        {
            get { return _bidWithFeeOwnValuta; }
            set
            {
                if (_bidWithFeeOwnValuta != value)
                {
                    _bidWithFeeOwnValuta = value;
                }
                Notify("bidWithFeeOwnValuta");
            }
        }

        private void CalculateAllValues()
        {
            bidEUR = bidUSD * valutaRates.rates["EUR"];
            bidOwnValuta = bidUSD * valutaRates.rates[ownValutaCode];

            bidWithFeeUSD = bidUSD * 1.0377D;
            bidWithFeeEUR = bidEUR * 1.0377D;
            bidWithFeeOwnValuta = bidOwnValuta * 1.0377D;
        }
    }
}

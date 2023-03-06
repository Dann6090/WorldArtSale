using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repository;

namespace IO
{
    public class ClassExchangeratesApi
    {
        ClassWebApiCall CWAC;
        public ClassExchangeratesApi()
        {
            CWAC = new ClassWebApiCall();
        }

        public async Task<ClassRates> GetDataFromOpenExchangeRatesAsync()
        {
            try
            {
                string strJson = await CWAC.GetDataFromWebApiAsync("https://openexchangerates.org/api/latest.json?app_id=ac5d2ecd34a44f688a4060555af1773e");
                return JsonConvert.DeserializeObject<ClassRates>(strJson);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }    
}

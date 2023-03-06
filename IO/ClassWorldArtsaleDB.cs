using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace IO
{
    public class ClassWorldArtsaleDB : ClassDbCon
    {
        //alt der for tingene til at ske mellem databasen og programmet
        public ClassWorldArtsaleDB()
        {
            //setter condition til at det skal være fra den database(navn på database)
            SetCon(@"Server=(localdb)\MSSQLLocalDB;DataBase=WorldArtSale;Trusted_Connection=True");
        }

        public List<ClassCountry> GetAllCountriesFromDB()
        {//liste til alle lande i databasen
            List<ClassCountry> listRes = new List<ClassCountry>();
            //try catch til at gøre koden mere rubust, den trier coden, catcher hvor ser om der er fejl, hvis der er smider den det væk og catcher hvorfra det sisdt virkede
            try
            {//Hvor den skal henta data fra. via Sql udtryk
                string sqlQuery = "SELECT * FROM Country";
                using (DataTable dataTable = DbReturnDataTable(sqlQuery))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ClassCountry cc = new ClassCountry();
                        cc.id = Convert.ToInt32(row["id"]);
                        cc.countryCode = row["code"].ToString();
                        cc.countryName = row["countryName"].ToString();
                        cc.valutaName = row["valutaName"].ToString();
                        listRes.Add(cc);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //retunere svar på listRes
            return listRes;
        }
        public List<ClassArt> GetAllArtFromDB()
        {
            List<ClassArt> listRes = new List<ClassArt>();
            try
            {//fra datatable i Sql server/databasen, hvor i at den selecter de følgende punkter hvor i at is active er active.
                string sqlQuery = "SELECT id, picturPath, descriptions, titel, isActive FROM ArtTable WHERE isActive = 1";
                using (DataTable dataTable = DbReturnDataTable(sqlQuery))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ClassArt ca = new ClassArt();
                        ca.id = Convert.ToInt32(row["id"]);
                        ca.picturePath = row["picturPath"].ToString();
                        ca.description = row["descriptions"].ToString();
                        ca.titel = row["titel"].ToString();
                        ca.isActive = Convert.ToBoolean(row["isActive"]);

                        listRes.Add(ca);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRes;
        }
        public List<ClassCustomer> GetAllCustomersFromDB()
        {
            List<ClassCustomer> listRes = new List<ClassCustomer>();
            try
            {//Sql databasen fra datatable
                string sqlQuery = "SELECT Customer.id, Customer.name, Customer.address, Customer.zipCity, Customer.country, Customer.email, Customer.phone, Customer.maximumBid, Country.id AS countryId, Country.code, Country.countryName, Country.valutaName, Customer.isActive FROM Customer LEFT OUTER JOIN Country ON Customer.preferredCurrency = Country.id WHERE (Customer.isActive = 1)";
                using (DataTable dataTable = DbReturnDataTable(sqlQuery))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ClassCustomer cc = new ClassCustomer();
                        cc.id = Convert.ToInt32(row["id"]);
                        cc.name = row["name"].ToString();
                        cc.address = row["address"].ToString();
                        cc.zipCity = row["zipCity"].ToString();
                        cc.country = row["country"].ToString();
                        cc.email = row["email"].ToString();
                        cc.phoneNo = row["phone"].ToString();
                        cc.maxBid = Convert.ToDouble(row["maximumBid"]);
                        ClassCountry cco = new ClassCountry();
                        cco.id = Convert.ToInt32(row["countryId"]);
                        cco.countryCode = row["code"].ToString();
                        cco.countryName = row["countryName"].ToString();
                        cco.valutaName = row["valutaName"].ToString();
                        cc.preferedCurrency = cco;
                        cc.isActive = Convert.ToBoolean(row["isActive"]);

                        listRes.Add(cc);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRes;
        }

        public int InsertCustomerInDB(ClassCustomer inCustomer)
        {
            int res = 0;
            string sqlQuery = "INSERT INTO Customer " +
                "(name, address, zipCity, country, email, phone, maximumBid, preferredCurrency, isActive) " +
                "VALUES (@name, @address, @zipCity, @country, @email, @phone, @maximumBid, @prefCurrency, @isActive) " +
                "SELECT SCOPE_IDENTITY()";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = inCustomer.name;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = inCustomer.address;
                    cmd.Parameters.Add("@zipCity", SqlDbType.NVarChar).Value = inCustomer.zipCity;
                    cmd.Parameters.Add("@country", SqlDbType.NVarChar).Value = inCustomer.country;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = inCustomer.email;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = inCustomer.phoneNo;
                    cmd.Parameters.Add("@maximumBid", SqlDbType.Money).Value = inCustomer.maxBid;
                    cmd.Parameters.Add("@prefCurrency", SqlDbType.Int).Value = inCustomer.preferedCurrency.id;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = inCustomer.isActive;

                    res = ExecuteScalarInDB(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public int UpdateCustomerDataInDB(ClassCustomer inCustomer)
        {
            int res = 0;
            string sqlQuery = "UPDATE Customer " +
                "SET " +
                "name = @name, " +
                "address = @address, " +
                "zipCity = @zipCity, " +
                "country = @country, " +
                "email = @email, " +
                "phone = @phone, " +
                "maximumBid = @maximumBid, " +
                "preferredCurrency = @prefCurrency, " +
                "isActive = @isActive " +
                "WHERE id = @id";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = inCustomer.name;
                    cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = inCustomer.address;
                    cmd.Parameters.Add("@zipCity", SqlDbType.NVarChar).Value = inCustomer.zipCity;
                    cmd.Parameters.Add("@country", SqlDbType.NVarChar).Value = inCustomer.country;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = inCustomer.email;
                    cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = inCustomer.phoneNo;
                    cmd.Parameters.Add("@maximumBid", SqlDbType.Money).Value = inCustomer.maxBid;
                    cmd.Parameters.Add("@prefCurrency", SqlDbType.Int).Value = inCustomer.preferedCurrency.id;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = inCustomer.isActive;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = inCustomer.id;

                    res = ExecuteNonQuery(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }

        public int InsertArtInDB(ClassArt inArt)
        {
            int res = 0;
            string sqlQuery = "INSERT INTO ArtTable " +
                "(picturPath, descriptions, titel, isActive) " +
                "VALUES (@picturPath, @descriptions, @titel, @isActive) " +
                "SELECT SCOPE_IDENTITY()";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@picturPath", SqlDbType.NVarChar).Value = inArt.picturePath;
                    cmd.Parameters.Add("@descriptions", SqlDbType.NVarChar).Value = inArt.description;
                    cmd.Parameters.Add("@titel", SqlDbType.NVarChar).Value = inArt.titel;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = inArt.isActive;

                    res = ExecuteScalarInDB(cmd);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return res;
        }

        public int UpdateArtInDB(ClassArt inArt)
        {
            int res = 0;
            string sqlQuery = "UPDATE ArtTable " +
                "SET picturPath = @picturPath, " +
                "descriptions = @descriptions, " +
                "titel = @titel, " +
                "isActive = @isActive " +
                "WHERE id = @id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@picturPath", SqlDbType.NVarChar).Value = inArt.picturePath;
                    cmd.Parameters.Add("@descriptions", SqlDbType.NVarChar).Value = inArt.description;
                    cmd.Parameters.Add("@titel", SqlDbType.NVarChar).Value = inArt.titel;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = inArt.isActive;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = inArt.id;

                    res = ExecuteNonQuery(cmd);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return res;
        }

        public int DeactivateArtInDB(int inID)
        {
            int res = 0;
            string sqlQuery = "UPDATE ArtTable " +
                "SET isActive = @isActive " +
                "WHERE id = @id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = false;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = inID;

                    res = ExecuteNonQuery(cmd);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return res;
        }
        public int BuyArt(ClassCustomer buyingCustomer, ClassArt boughtArt, ClassBidCalck inBid)
        {
            int res = 0;
            string sqlQuery = "INSERT INTO ArtTrade " +
                "(customerID, artID, customerBidUSD, customerBidEUR, customerBidOwnValuta, " +
                "priceWithFeesUSD, priceWithFeesEUR, priceWithFeesOwnValuta, date) " +
                "VALUES " +
                "(@customerID, @artID, @customerBidUSD, @customerBidEUR, @customerBidOwnValuta, " +
                "@priceWithFeesUSD, @priceWithFeesEUR, @priceWithFeesOwnValuta, @date)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.Add("@customerID", SqlDbType.Int).Value = buyingCustomer.id;
                    cmd.Parameters.Add("@artID", SqlDbType.Int).Value = boughtArt.id;
                    cmd.Parameters.Add("@customerBidUSD", SqlDbType.Money).Value = inBid.bidUSD;
                    cmd.Parameters.Add("@customerBidEUR", SqlDbType.Money).Value = inBid.bidEUR;
                    cmd.Parameters.Add("@customerBidOwnValuta", SqlDbType.Money).Value = inBid.bidOwnValuta;
                    cmd.Parameters.Add("@priceWithFeesUSD", SqlDbType.Money).Value = inBid.bidWithFeeUSD;
                    cmd.Parameters.Add("@priceWithFeesEUR", SqlDbType.Money).Value = inBid.bidWithFeeEUR;
                    cmd.Parameters.Add("@priceWithFeesOwnValuta", SqlDbType.Money).Value = inBid.bidWithFeeOwnValuta;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = DateTime.Now;

                    res = ExecuteNonQuery(cmd);
                }
                DeactivateArtInDB(boughtArt.id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }
    }
}

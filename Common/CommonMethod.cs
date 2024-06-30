using System.Data.SqlClient;
using System.Data;
using Trip2.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Trip2.Common
{
    public class CommonMethod
    {

        #region LoadPlaceFilter
        /* public void LoadPlaceFilter()
         {
             SqlConnection sqlconnection = new SqlConnection(this.Configuration.GetConnectionString("myConnectionString"));

             sqlconnection.Open();

             SqlCommand sqlcmd1 = sqlconnection.CreateCommand();
             sqlcmd1.CommandType = CommandType.StoredProcedure;
             sqlcmd1.CommandText = "[dbo].[PR_Filter_CityName_City]";
             SqlDataReader sqlDataReader1 = sqlcmd1.ExecuteReader();
             DataTable cityDt = new DataTable();

             cityDt.Load(sqlDataReader1);
             List<CityModel> CityList = new List<CityModel>();
             foreach (DataRow dr in cityDt.Rows)
             {
                 CityModel cityObj = new CityModel();
                 cityObj.CityID = Convert.ToInt32(dr["CityID"]);
                 cityObj.CityName = dr["CityName"].ToString();
                 CityList.Add(cityObj);
             }
             ViewBag.CityList = CityList;

             SqlCommand sqlcmd2 = sqlconnection.CreateCommand();
             sqlcmd2.CommandType = CommandType.StoredProcedure;
             sqlcmd2.CommandText = "[dbo].[PR_Filter_StateName_State]";
             SqlDataReader sqlDataReader2 = sqlcmd2.ExecuteReader();
             DataTable stateDt = new DataTable();
             stateDt.Load(sqlDataReader2);
             List<StateModel> StateList = new List<StateModel>();
             foreach (DataRow dr in stateDt.Rows)
             {
                 StateModel stateObj = new StateModel();
                 stateObj.StateID = Convert.ToInt32(dr["StateID"]);
                 stateObj.StateName = dr["StateName"].ToString();
                 StateList.Add(stateObj);
             }
             ViewBag.StateList = StateList;


             SqlCommand sqlcmd3 = sqlconnection.CreateCommand();
             sqlcmd3.CommandType = CommandType.StoredProcedure;
             sqlcmd3.CommandText = "[dbo].[PR_Filter_CountryName_Country]";
             SqlDataReader sqlDataReader3 = sqlcmd3.ExecuteReader();
             DataTable countryDt = new DataTable();
             countryDt.Load(sqlDataReader3);
             List<CountryModel> CountryList = new List<CountryModel>();
             foreach (DataRow dr in countryDt.Rows)
             {
                 CountryModel countryObj = new CountryModel();
                 countryObj.CountryID = Convert.ToInt32(dr["CountryID"]);
                 countryObj.CountryName = dr["CountryName"].ToString();
                 CountryList.Add(countryObj);
             }


             ViewBag.CountryList = CountryList;

         }*/
        #endregion


    }


}

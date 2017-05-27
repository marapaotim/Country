using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using System.Web.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index(string searchBy)
        {
            if(searchBy == "Poland")
            {
                ViewBag.countries = CountryList().Where(x => x.CountryName == "Poland");
                return View();
            } 
            ViewBag.countries = CountryList();
            return View();  
        }

        List<CountryModel> ModelCountryArray = new List<CountryModel>();
        CountryModel MCountry = new CountryModel();

        public static string GetJsonData()
        {
            string result = "";
            try
            {
                string USPS = "https://restcountries.eu/rest/v2/all";
                WebClient wsClient = new WebClient();
                byte[] responseData = wsClient.DownloadData(USPS);
                string response = string.Empty;
                foreach (byte item in responseData)
                {
                    response += (char)item;
                }
                result = response;
            }
            catch (Exception ex)
            {
                //Console.Write("Error" + ex.ToString());
            }
            return result;
        }

        public List<CountryModel> CountryList()
        {
            dynamic stuff = JsonConvert.DeserializeObject(GetJsonData());
            foreach (var data in stuff)
            { 
                MCountry.CountryName = data.name;
                try
                {
                    MCountry.topLevelDomain = data.topLevelDomain[0]; 
                }
                catch
                {
                    MCountry.topLevelDomain = "N/A";
                }
                MCountry.alpha2Code = data.alpha2Code;
                MCountry.alpha3Code = data.alpha3Code;
                try
                {
                    MCountry.callingCodes = data.callingCodes[0];
                }
                catch
                {
                    MCountry.callingCodes = "N/A";
                }

                MCountry.capital = data.capital;
                try
                {
                    MCountry.altSpellings = data.altSpellings[0];
                }
                catch
                {
                    MCountry.altSpellings = "N/A";
                }
                MCountry.region = data.region;
                MCountry.subregion = data.subregion;
                MCountry.population = data.population;
                try
                {
                    MCountry.latlng = data.latlng[0];
                }
                catch
                {
                    MCountry.latlng = "N/A";
                }
                MCountry.demonym = data.demonym;
                MCountry.area = data.area;
                MCountry.gini = data.gini;
                try
                {
                    MCountry.timezones = data.timezones[0];
                }
                catch
                {
                    MCountry.timezones = "N/A";
                }
                try
                {
                    MCountry.borders = data.borders[0];
                }
                catch
                {
                    MCountry.borders = "N/A";
                }
                    
                MCountry.nativeName = data.nativeName;
                MCountry.numericCode = data.numericCode;
                try
                { 
                    MCountry.currencies = data.currencies[0].code;
                }
                catch
                {
                    MCountry.currencies = "N/A";
                }
                try
                {
                    MCountry.languages = data.languages[0].name;
                }
                catch
                {
                    MCountry.languages = "N/A";
                }
                MCountry.translations = data.translations.de;
                MCountry.flag = data.flag;
                try
                {
                    MCountry.regionalBlocs = data.regionalBlocs[0].acronym;
                }
                catch
                {
                    MCountry.regionalBlocs = "N/A";
                }
                ModelCountryArray.Add(MCountry);
            }
            return ModelCountryArray;
        }  
    }
}
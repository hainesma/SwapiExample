using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SwapiExample.Models
{
    // DAL - Data Access Layer
    // It's a common pattern designed to keep the code for pulling external data
    // Separate from the rest of the project and all in one location
    // That way we can reuse the DAL wherever we want in one project
    public class StarWarsDAL
    {
        // This method will call our API
        // and return our JSON all in one
        public string CallSWAPI(int id, string endpoint)
        {
            string url = @$"https://swapi.dev/api/{endpoint}/{id}";

            // This line creates the request
            HttpWebRequest request = WebRequest.CreateHttp(url);

            // This line send that request to the SWAPI server
            // and grabs its response
            // which will be either an error or JSON data
            // Common Error Codes:
            // 404 Not Found
            // 500 Server side error
            // 300 Server has moved, or redirect
            // 200 Successful request
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // This grabs the file out of the response
            StreamReader rd = new StreamReader(response.GetResponseStream());

            string JSON = rd.ReadToEnd();
            rd.Close();

            return JSON;
        }

        // For each endpoint you want to query
        // You will need to create a method to parse that endpoint's data

        public SwapiParent GetModel(int id, string endpoint) 
        {
            string personJson = CallSWAPI(id, endpoint);

            // This method call takes in our raw JSON and
            // we tell it which model to try to output the data to.
            SwapiParent p = JsonConvert.DeserializeObject<Person>(personJson);

            return p;
        }

    }
}

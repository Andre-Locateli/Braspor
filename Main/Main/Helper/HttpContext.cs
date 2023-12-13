using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Remoting;
using System.Net;

namespace Main.Helper
{
    public class HttpContext
    {
        //Pegar esses dados da tela de configuração.

        private string yourusername = "admin";
        //private string yourusername = "admin";
        private string yourpwd = "aeph2022@";

        public async Task<List<object>> Get_Values_l(string URL)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{yourusername}:{yourpwd}")));
                using (var response = await client.GetAsync(URL))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        List<object> List = JsonConvert.DeserializeObject<List<object>>(ProdutoJsonString);
                        return (List<object>)List;
                    }
                    else
                        MessageBox.Show("Não foi possível obter o produto : " + response.StatusCode);
                }
            }
            return new List<object>();
        }

        public async Task<string> Get_Values_s(string URL)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{yourusername}:{yourpwd}")));
                client.Timeout = TimeSpan.FromSeconds(2);
                using (var response = await client.GetAsync(URL))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        return ProdutoJsonString;
                    }
                    else
                    {
                        return "Bad Request Protheus";
                    }
                        //MessageBox.Show("Não foi possível obter o produto : " + response.StatusCode);
                }
            }
            return "";
        }

        public async Task<bool> Post_Value(string URL, string parameters)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{yourusername}:{yourpwd}")));

                var content = new StringContent(parameters.ToString(), Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(URL, content).ConfigureAwait(false))
                {
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        var resp = await response.Content.ReadAsStringAsync();
                        //return ProdutoJsonString;

                        if (resp.Contains("true"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else 
                    {
                        return false;
                    }
                }
            }
        }

        //public async Task<Dictionary<string,string>> WeatherAPI(string city)
        //{
        //    try
        //    {
        //        var accessKey = "a86baf02eab8397594aaa94ddafcea68";
        //        var url = $"http://api.weatherstack.com/current?access_key={accessKey}&query={city}";
        //        var client = new HttpClient();
        //        var response = await client.GetAsync(url);
        //        var content = await response.Content.ReadAsStringAsync();
        //        var data = JObject.Parse(content);

        //        Dictionary<string, string> dic_response = new Dictionary<string, string>();

        //        //dic_response.Add("temperature", Convert.ToString((float)data["current"]["temperature"]));
        //        //dic_response.Add("weather_icons", Convert.ToString((float)data["current"]["weather_icons"]));
        //        dic_response.Add("weather_descriptions", Convert.ToString((float)data["current"]["weather_descriptions"]));
        //        dic_response.Add("wind_speed", Convert.ToString((float)data["current"]["wind_speed"]));
        //        dic_response.Add("wind_degree", Convert.ToString((float)data["wind_degree"]["wind_degree"]));

        //        return dic_response;
        //    }
        //    catch (Exception)
        //    {
        //        return new Dictionary<string, string>();
        //    }
        //}

    }
}

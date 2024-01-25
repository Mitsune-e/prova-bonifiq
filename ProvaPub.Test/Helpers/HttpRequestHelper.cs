using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProvaPub.Test.Helpers
{
    public static class HttpRequestHelper
    {
        public static dynamic Get(string url) =>
            Get<dynamic>(url);

        public static dynamic Get<T>(string url)
        {
            string resultado = null;
            try
            {
                var request = WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                request.ContentType = "application/json;";
                request.Timeout = 600000; // 10 minutes

                var response = (HttpWebResponse)request.GetResponse();
                var encodingString = string.IsNullOrEmpty(response.CharacterSet) ? Encoding.UTF8.HeaderName : response.CharacterSet;

                using (var streamReader = new StreamReader(response.GetResponseStream(), encoding: Encoding.GetEncoding(encodingString)))
                    resultado = streamReader.ReadToEnd();

                var status = response.StatusCode;

                response.Close();

                if (status != HttpStatusCode.OK)
                    throw new Exception("Erro ao acessar o Sistema.");

                if (typeof(T) == typeof(string))
                    return resultado;

                dynamic obj = JsonConvert.DeserializeObject<T>(resultado);

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao acessar a API. Url: {url} . UrlResultado: {resultado} . Erro: {ex.ToString()}", ex);
            }
        }

    }
}

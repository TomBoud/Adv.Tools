
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.Autodesk.AppStore
{
    public class EntitlementApi
    {

        private readonly string _baseUrl;
        private readonly string _endPoint;

        public EntitlementModel EntitlementResposne { get; private set; }

        public EntitlementApi(string baseUrl, string endPoint)
        {
            _baseUrl = baseUrl;
            _endPoint = endPoint;

            EntitlementResposne = new EntitlementModel();
        }

        public bool CheckEntitlement(string appId, string userId)
        {
            try
            {
                //Build request
                var client = new RestClient(_baseUrl);
                var request = new RestRequest();
                request.Resource = _endPoint;
                request.Method = Method.Get;

                //Add parameters
                request.AddParameter(nameof(userId), userId);
                request.AddParameter(nameof(appId), appId);

                //Execute request and get response
                var response = client.Execute(request);

                //Parse the response and get the value of IsValid.
                if (response.IsSuccessStatusCode)
                {
                    EntitlementResposne = JsonConvert.DeserializeObject<EntitlementModel>(response.Content);
                    return EntitlementResposne.IsValid;
                }
                else
                {
                    throw new Exception($"{response.StatusCode}");
                }
            }
            catch (WebException)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}

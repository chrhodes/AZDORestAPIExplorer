using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core
{
    public class Helpers
    {

        public static void InitializeHttpClient(Organization collection, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            string base64PAT = Convert.ToBase64String(
                    ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", collection.PAT)));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);
        }

    }
}

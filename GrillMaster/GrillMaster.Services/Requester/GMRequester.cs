#region [Imports]

using System;
using System.IO;
using System.Net;
using System.Xml;

#endregion

namespace GrillMaster.Services.Requester
{
    public class GMRequester
    {
        private static readonly Uri serverBaseUri = new Uri(Properties.Settings.Default.ServerBaseUri);
        private static CredentialCache Cache;

        #region [Constructors]

        public GMRequester(string userName, string userPassword)
        {
            var serviceCreds = new NetworkCredential(userName, userPassword);
            Cache = new CredentialCache { { serverBaseUri, "Basic", serviceCreds } };
        }

        public static XmlDocument MakeRequest(string entityUriParameter)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(CreateRequest(entityUriParameter));

            return xmlDocument;
        }

        private static Stream CreateRequest(string entityUriParameter)
        {
            var resultUri = new Uri(string.Format("{0}/{1}", serverBaseUri, entityUriParameter));
            var httpRequest = (HttpWebRequest)WebRequest.Create(resultUri);
            httpRequest.Credentials = Cache;

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            return httpResponse.GetResponseStream();
        }

        #endregion
    }
}

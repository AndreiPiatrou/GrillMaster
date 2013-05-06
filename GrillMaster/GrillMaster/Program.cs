using System;
using GrillMaster.Services.Requester;

namespace GrillMaster
{
    public class Program
    {
        private static void Main(string[] args)
        {
            const string userName = "a.petrov@itransition.com";
            const string password = "Pjxi6";

            Console.WriteLine("User name: {0}", userName);
            Console.WriteLine("Password:  {0}", password);
            GMRequester.InitRequester(userName, password);
            var request = GMRequester.MakeRequest(string.Empty);

            Console.WriteLine(request.InnerXml);
            Console.ReadKey();
        }
    }
}

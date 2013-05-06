using GrillMaster.Services.Requester;

namespace GrillMaster
{
    public class Program
    {
        private static void Main(string[] args)
        {
            GMRequester.InitRequester("a.petrov@itransition.com", "Pjxi6");
            var request = GMRequester.MakeRequest(string.Empty);
        }
    }
}

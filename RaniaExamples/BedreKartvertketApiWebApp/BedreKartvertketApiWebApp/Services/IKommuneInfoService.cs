using BedreKartvertketApiWebApp.API_Models;

namespace BedreKartvertketApiWebApp.Services
{
    public interface IKommuneInfoService
    {
        Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr);
    }
}

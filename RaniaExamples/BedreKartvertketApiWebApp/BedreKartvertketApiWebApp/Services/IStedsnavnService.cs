using BedreKartvertketApiWebApp.API_Models;

namespace BedreKartvertketApiWebApp.Services
{
    public interface IStedsnavnService
    {
        Task<StedsnavnResponse> GetStedsnavnAsync(string search);
    }
}

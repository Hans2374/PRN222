using Repositories.Models;

namespace Services.Interfaces
{
    public interface ILionProfileService
    {
        Task<List<LionProfile>> GetAllProfilesAsync();
        Task<LionProfile?> GetProfileByIdAsync(int id);
        Task<(List<LionProfile> profiles, int totalPages)> GetPaginatedProfilesAsync(int pageNumber, int pageSize = 3);
        Task<List<LionProfile>> SearchProfilesAsync(double? weight, string? lionTypeName);
        Task<bool> CreateProfileAsync(LionProfile profile);
        Task<bool> UpdateProfileAsync(LionProfile profile);
        Task<bool> DeleteProfileAsync(int id);
        Task<bool> ValidateLionProfile(LionProfile profile);
        string? ValidateLionName(string lionName);
    }
}
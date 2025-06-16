using Repositories.Models;
using Repositories.UnitOfWork;
using Services.Interfaces;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Services.Implementations
{
    public class LionProfileService : ILionProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LionProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LionProfile>> GetAllProfilesAsync()
        {
            return await _unitOfWork.LionProfiles.GetAllWithTypeAsync();
        }

        public async Task<LionProfile?> GetProfileByIdAsync(int id)
        {
            return await _unitOfWork.LionProfiles.GetByIdWithTypeAsync(id);
        }

        public async Task<(List<LionProfile> profiles, int totalPages)> GetPaginatedProfilesAsync(int pageNumber, int pageSize = 3)
        {
            var (profiles, totalCount) = await _unitOfWork.LionProfiles.GetPaginatedAsync(pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return (profiles, totalPages);
        }

        public async Task<List<LionProfile>> SearchProfilesAsync(double? weight, string? lionTypeName)
        {
            return await _unitOfWork.LionProfiles.SearchAsync(weight, lionTypeName);
        }

        public async Task<bool> CreateProfileAsync(LionProfile profile)
        {
            try
            {
                if (!await ValidateLionProfile(profile))
                    return false;

                profile.ModifiedDate = DateTime.Now;
                await _unitOfWork.LionProfiles.CreateAsync(profile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProfileAsync(LionProfile profile)
        {
            try
            {
                if (!await ValidateLionProfile(profile))
                    return false;

                profile.ModifiedDate = DateTime.Now;
                await _unitOfWork.LionProfiles.UpdateAsync(profile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            return await _unitOfWork.LionProfiles.DeleteAsync(id);
        }

        public async Task<bool> ValidateLionProfile(LionProfile profile)
        {
            if (profile == null)
                return false;

            // Validate LionName
            var nameValidation = ValidateLionName(profile.LionName);
            if (!string.IsNullOrEmpty(nameValidation))
                return false;

            // Validate Weight > 30
            if (profile.Weight <= 30)
                return false;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(profile.Characteristics) ||
                string.IsNullOrWhiteSpace(profile.Warning))
                return false;

            // Validate LionType exists
            var lionType = await _unitOfWork.LionTypes.GetByIdAsync(profile.LionTypeId);
            if (lionType == null)
                return false;

            return true;
        }

        public string? ValidateLionName(string lionName)
        {
            if (string.IsNullOrWhiteSpace(lionName))
                return "Lion name is required";

            if (lionName.Length <= 3)
                return "Lion name must be greater than 3 characters";

            // Check for special characters
            if (Regex.IsMatch(lionName, @"[#@&()]"))
                return "Lion name cannot contain special characters (#, @, &, (, ))";

            // Check if each word starts with capital letter
            var words = lionName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                if (!char.IsUpper(word[0]))
                    return "Each word of the Lion name must begin with a capital letter";
            }

            return null; // Valid
        }
    }
}
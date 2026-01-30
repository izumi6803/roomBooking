using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IFacilityTypeRepository
    {
        Task<FacilityType?> GetByIdAsync(string id);
        Task<(List<FacilityType> items, int total)> GetPagedAsync(int page, int limit);
        Task<int> CreateAsync(FacilityType entity);
        Task<int> UpdateAsync(FacilityType entity);
        Task<List<FacilityType>> GetAllAsync();
        Task<int> DeleteAsync(FacilityType entity);
    }
}



using uBee.Domain.Entities;

namespace uBee.Domain.Repositories
{
    public interface IBeeContractRepository
    {
        #region IBeeContractRepository Members

        Task<BeeContract> GetByIdAsync(Guid idContract);
        Task<IEnumerable<BeeContract>> GetContractsByUserIdAsync(Guid userId);
        Task<IEnumerable<BeeContract>> GetActiveContractsAsync();
        void Insert(BeeContract contract);
        Task UpdateAsync(BeeContract contract);

        #endregion
    }
}

using uBee.Domain.Entities;

namespace uBee.Domain.Repositories
{
    public interface IContractedHiveRepository
    {
        #region IContractedHiveRepository Members

        Task<ContractedHive> GetByIdAsync(Guid idContractedHive);
        Task<IEnumerable<ContractedHive>> GetByContractIdAsync(Guid contractId);
        void Insert(ContractedHive contractedHive);
        Task UpdateAsync(ContractedHive contractedHive);

        #endregion
    }
}

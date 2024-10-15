using uBee.Domain.Entities;

namespace uBee.Domain.Repositories
{
    public interface IHiveRepository
    {
        #region IHiveRepository Members

        Task<Hive> GetByIdAsync(Guid idHive);
        Task<IEnumerable<Hive>> GetHivesByUserIdAsync(Guid userId);
        Task<IEnumerable<Hive>> GetAvailableHivesAsync();
        void Insert(Hive hive);
        Task UpdateAsync(Hive hive);

        #endregion
    }
}

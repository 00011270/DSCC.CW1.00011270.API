namespace BlogPlatform.Repository
{

    // Interface Repository that other Model based Repo classes will implement
    public interface IRepository<T> where T : class
    {
        Task InsertObject(T obj);
        Task DeleteObject(int objId);
        Task UpdateObject(T obj);
        Task<T> GetObjectById(int obj);
        Task<IEnumerable<T>> GetObjectList();
    }
}

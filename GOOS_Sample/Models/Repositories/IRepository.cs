namespace GOOS_Sample.Models.Repositories
{
    public interface IRepository<T>
    {
        void Save(T budget);
    }
}
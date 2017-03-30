using System.Collections.Generic;

namespace Blog40.Repository
{
    public interface IRepository<T>
    {
        int Add(T model);

        IEnumerable<T> GetAll();

        T Get(int Id);

        T GetByString(string identifier);

        int Update(T model);

        int Delete(int Id);

        int UndeleteAll();
    }
}

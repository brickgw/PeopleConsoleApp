using System.Collections.Generic;

namespace CSECodeSampleConsole
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        void Create(string name);
        bool TryFind(string name, out List<T> entities);
    }
}
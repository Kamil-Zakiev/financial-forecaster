using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FF.Engine
{
    public interface ISpecificAccountant<T>
    {
        Task Add(T item);
        Task<IReadOnlyCollection<T>> GetAll();
        Task Update(T item, Action<T> updater);
        Task Delete(T item);
    }
}
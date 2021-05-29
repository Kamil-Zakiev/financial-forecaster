using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FF.Engine
{
    internal sealed class SpecificAccountant<T> : ISpecificAccountant<T>
    {
        private readonly LinkedList<T> _repo = new(); 
        
        public Task Add(T item)
        {
            _repo.AddLast(item);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<T>> GetAll()
        {
            return Task.FromResult<IReadOnlyCollection<T>>(_repo);
        }

        public Task Update(T item, Action<T> updater)
        {
            updater(item);
            return Task.CompletedTask;
        }

        public Task Delete(T item)
        {
            _repo.Remove(item);
            return Task.CompletedTask;
        }
    }
}
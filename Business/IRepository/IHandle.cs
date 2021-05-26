namespace Business.IRepository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHandle<T>
    {
        public Task<T> Create(T entity);

        public Task<T> Update(T entity);

        public Task<int> Delete(Guid id);

        public Task<T> Get(Guid id);

        public Task<ICollection<T>> GetAll();
    }
}

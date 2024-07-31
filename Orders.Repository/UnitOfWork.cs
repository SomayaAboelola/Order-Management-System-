using Orders.Core.Entities;
using Orders.Core.Repository.Contract;
using Orders.Repository._Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext _context;
        private Hashtable _repository;

        public UnitOfWork(OrderDbContext context) 
        {
            _context = context;
            _repository = new Hashtable();  
        }
        public async Task<int> CompleteAsync()
        {
          return await _context.SaveChangesAsync();
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        public  IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
           var key = typeof(T).Name;

            if(!_repository.ContainsKey(key))
            {
                var repository = new GenericRepository<T>(_context);
                _repository.Add(key, repository);

            }
            return  _repository[key] as IGenericRepository<T>;

        }

      
    }
}

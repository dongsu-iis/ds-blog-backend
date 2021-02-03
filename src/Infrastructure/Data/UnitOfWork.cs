using SharedKernel;
using SharedKernel.Interfaces;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _context;
        private Hashtable _repositories;

        public UnitOfWork(BlogContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositortyType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositortyType.MakeGenericType(typeof(TEntity)),_context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }
    }
}

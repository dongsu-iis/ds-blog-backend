using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Specifications;
using SharedKernel.Interfaces;
using Type = Core.Entities.Type;

namespace Core.Services
{
    public class TypeSearchService : ITypeSearchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeSearchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Type>> GetAsync(TypeSpecParams typeParams)
        {
            var spec = new TypeSpecification(typeParams);
            var types = await _unitOfWork.Repository<Type>().ListAsync(spec);
            return types;
        }

        public async Task<int> CountAsync(TypeSpecParams typeParams)
        {
            var spec = new TypeSpecification(typeParams);
            var count = await _unitOfWork.Repository<Type>().CountAsync(spec);
            return count;
        }
    }
}

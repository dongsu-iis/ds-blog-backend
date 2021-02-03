using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Specifications;
using Type = Core.Entities.Type;

namespace Core.Interfaces
{
    public interface ITypeSearchService
    {
        Task<IReadOnlyList<Type>> GetAsync(TypeSpecParams typeParams);
        Task<int> CountAsync(TypeSpecParams typeParams);
    }
}

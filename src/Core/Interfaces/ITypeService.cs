using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Specifications;
using Type = Core.Entities.Type;

namespace Core.Interfaces
{
    public interface ITypeService
    {
        Task<IReadOnlyList<Type>> GetPagedByParamAsync(TypeSpecParams typeParams);
        Task<int> CountByParamAsync(TypeSpecParams typeParams);

        Task<Type> FindByIdAsync(int id);

        Task<IResult> UpdateAsync(Type type);

        Task<IResult> CreateAsync(Type type);

        Task<IResult> DeleteAsync(Type type);
    }
}

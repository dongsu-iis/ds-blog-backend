using SharedKernel;
using Type = Core.Entities.Type;

namespace Core.Specifications
{
    public class TypeSpecification : BaseSpecification<Type>
    {
        public TypeSpecification(TypeSpecParams typeParams)
            :base(x=>(string.IsNullOrEmpty(typeParams.Search)||x.Name.ToLower().Contains(typeParams.Search)))
        {
            AddOrderBy(x => x.Name);
            ApplyPaging(typeParams.PageSize * (typeParams.PageIndex - 1), typeParams.PageSize);

        }
    }
}

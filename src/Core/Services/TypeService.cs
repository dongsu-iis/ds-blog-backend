using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Specifications;
using SharedKernel.Interfaces;
using Type = Core.Entities.Type;

namespace Core.Services
{
    public class TypeService : ITypeService
    {

        private readonly IUnitOfWork _unitOfWork;

        public TypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Type>> GetPagedByParamAsync(TypeSpecParams typeParams)
        {
            var spec = new TypeSpecification(typeParams);
            var types = await _unitOfWork.Repository<Type>().ListAsync(spec);
            return types;
        }

        public async Task<int> CountByParamAsync(TypeSpecParams typeParams)
        {
            var spec = new TypeSpecification(typeParams);
            var count = await _unitOfWork.Repository<Type>().CountAsync(spec);
            return count;
        }

        public async Task<Type> FindByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Type>().GetByIdAsync(id);
        }


        public async Task<IResult> CreateAsync(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            IResult result = new Result(false);

            try
            {
                _unitOfWork.Repository<Type>().Add(type);
                await _unitOfWork.Complete();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;

        }

        public async Task<IResult> UpdateAsync(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            IResult result = new Result(false);

            try
            {
                _unitOfWork.Repository<Type>().Update(type);
                await _unitOfWork.Complete();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;

        }

        public async Task<IResult> DeleteAsync(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            IResult result = new Result(false);

            try
            {
                _unitOfWork.Repository<Type>().Delete(type);
                await _unitOfWork.Complete();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }
    }
}

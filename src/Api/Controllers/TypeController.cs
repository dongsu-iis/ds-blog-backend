using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Type = Core.Entities.Type;
using Core.Interfaces;
using AutoMapper;
using Api.Helpers;
using Api.Dtos;
using Core.Specifications;
using Api.Errors;

namespace Api.Controllers
{


    public class TypeController : BaseApiController
    {
        private readonly ITypeService _typeService;
        private readonly IMapper _mapper;


        public TypeController(ITypeService typeService, IMapper mapper)
        {
            _typeService = typeService;
            _mapper = mapper;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<Pagination<TypeDto>>> GetType([FromQuery] TypeSpecParams typeSpecParams)
        {
            var types = await _typeService.GetPagedByParamAsync(typeSpecParams);
            var count = await _typeService.CountByParamAsync(typeSpecParams);
            var data = _mapper.Map<IReadOnlyList<Type>, IReadOnlyList<TypeDto>>(types);
            return Ok(new Pagination<TypeDto>(typeSpecParams.PageIndex, typeSpecParams.PageSize, count, data));
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeDto>> GetType(int id)
        {
            var type = await _typeService.FindByIdAsync(id);

            if (type == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return _mapper.Map<Type, TypeDto>(type);
        }

        // PUT: api/Types/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<TypeDto>> UpdateType(TypeDto typeDto)
        {

            var type = _mapper.Map<TypeDto,Type>(typeDto);

            var result = await _typeService.UpdateAsync(type);

            return result.Success ? Ok(_mapper.Map<Type, TypeDto>(type))
                                  : BadRequest(new ApiResponse(400,result.Message));
        }

        // POST: api/Types
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeDto>> CreateType(TypeDto typeDto)
        {
            var type = new Type
            {
                Name = typeDto.Name
            };

            var result = await _typeService.CreateAsync(type);

            return result.Success ? CreatedAtAction(nameof(CreateType), new { id = type.Id }, _mapper.Map<Type, TypeDto>(type))
                                  : BadRequest(new ApiResponse(400, result.Message));

        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteType(int id)
        {
            var type = await _typeService.FindByIdAsync(id);

            if (type == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var result = await _typeService.DeleteAsync(type);

            return result.Success ? Ok(_mapper.Map<Type, TypeDto>(type))
                                  : BadRequest(new ApiResponse(400, result.Message));

        }

    }
}

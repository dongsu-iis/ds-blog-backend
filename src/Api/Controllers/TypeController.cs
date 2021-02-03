using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Helpers;
using AutoMapper;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Type = Core.Entities.Type;

namespace Api.Controllers
{
    public class TypeController : BaseApiController
    {
        private readonly ITypeSearchService _typeSearchService;
        private readonly IMapper _mapper;

        public TypeController(ITypeSearchService typeSearchService,IMapper mapper)
        {
            _typeSearchService = typeSearchService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<TypeDto>>> GetTypes([FromQuery]TypeSpecParams typeSpecParams)
        {
           var types = await _typeSearchService.GetAsync(typeSpecParams);
           var count = await _typeSearchService.CountAsync(typeSpecParams);
           var data = _mapper.Map<IReadOnlyList<Type>, IReadOnlyList<TypeDto>>(types);
           return Ok(new Pagination<TypeDto>(typeSpecParams.PageIndex, typeSpecParams.PageSize, count, data));
        }
    }
}

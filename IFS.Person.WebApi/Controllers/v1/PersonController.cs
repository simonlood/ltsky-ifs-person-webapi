using AutoMapper;
using IFS.Person.WebApi.Domain.Services.Interfaces;
using IFS.Person.WebApi.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using IFS.Person.WebApi.DataTransferObjects;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace IFS.Person.WebApi.Controllers.v1;

[DomainApiKeyAuth]
public partial class PersonController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService, IMapper mapper)
    {
        _personService = personService;
        _mapper = mapper;
    }

    [HttpGet("ActiveUser")]
    [SwaggerOperation(Summary = "Get Active Users", Description = "Get active users from IFS")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error occurred")]
    public async Task<IActionResult> GetActiveUsersAsync(string active)
    {
        try
        {
            var result = _mapper.Map<List<ActiveUserDto>>(await _personService.GetActiveUsersAsync(active));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PersonGeneralInfo")]
    [SwaggerOperation(Summary = "Get Person General Info", Description = "Get person general info from IFS")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error occurred")]
    public async Task<IActionResult> GetPersonGeneralInfoAsync(string userId)
    {
        try
        {
            var result = _mapper.Map<PersonGeneralInfoDto>(await _personService.GetPersonGeneralInfoAsync(userId));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PersonGeneralInfoCollection")]
    [SwaggerOperation(Summary = "Get Person General Info Collection", Description = "Get person general info collection from IFS")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error occurred")]
    public async Task<IActionResult> GetPersonGeneralInfoAsync()
    {
        try
        {
            var result = _mapper.Map<List<PersonGeneralInfoDto>>(await _personService.GetPersonGeneralInfoAsync());
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PersonCompanyInfo")]
    [SwaggerOperation(Summary = "Get Person Company Info", Description = "Get person company info from IFS")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error occurred")]
    public async Task<IActionResult> GetPersonCompanyInfoAsync(string personId)
    {
        try
        {
            var result = _mapper.Map<List<PersonCompanyInfoDto>>(await _personService.GetPersonCompanyInfoAsync(personId));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
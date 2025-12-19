using Microsoft.AspNetCore.Mvc;

namespace IFS.Person.WebApi.Controllers.v1;

[ApiController]
[Route("v1/[controller]")]
public abstract class BaseController : ControllerBase
{
}
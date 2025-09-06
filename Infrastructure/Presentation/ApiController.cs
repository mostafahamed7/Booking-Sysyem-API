using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]

    [ProducesResponseType(typeof(ErrorDetailes), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetailes), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErorrResponse), (int)HttpStatusCode.BadRequest)]
    public class ApiController : ControllerBase
    {
    }
}

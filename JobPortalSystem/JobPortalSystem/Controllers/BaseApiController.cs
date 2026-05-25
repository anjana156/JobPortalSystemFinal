using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalSystem.Controllers
{

    [Route("api/v1")]
    public abstract class BaseApiController<T> : ControllerBase
    {

    }
}

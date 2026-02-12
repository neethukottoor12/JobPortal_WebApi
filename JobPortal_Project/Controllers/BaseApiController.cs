using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal_Project.Controllers
{
    [Route("api/v1")]
    
    public abstract class BaseApiController<T> : ControllerBase
    {
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Magic8Ball.Api.Controllers 
{
    /// <summary>
    /// Base controller.    
    /// </summary>
    //[Route("api/v1/[controller]")]
    //[ApiController]
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
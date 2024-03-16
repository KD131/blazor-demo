using Microsoft.AspNetCore.Mvc;
using Model.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    [HttpGet("demos")]
    public List<Post> Demos()
    {
        return [];
    }
}
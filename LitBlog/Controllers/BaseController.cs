using LitBlog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LitBlog.API.Controllers
{

    [Controller]
    public class BaseController : ControllerBase
    {
        public AccountViewModel Account => (AccountViewModel) HttpContext.Items["Account"];
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intento001.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Intento001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        
        [HttpGet]
        public string Get()
        {
            return AuthenticationConfig.CreateJsonWebToken();
        }

        
        [Authorize]
        [HttpPost]
        public string Post()
        {
            return "true";
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public string Get(string user, string pass)
        {
            if (user == "Admin" && pass =="123")
                return AuthenticationConfig.CreateJsonWebToken(user,pass);
            else
                return "";
        }

        
        [Authorize]
        [HttpPost]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var username = claims.Where(x => x.Type == "userName").Select(c => c.Value).SingleOrDefault();
            return $"Bien venido {username}";
        }

       
    }
}

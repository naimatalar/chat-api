using chat.Entites.Context;
using Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ChatContext _context;

        public HomeController(ChatContext context)
        {
            _context = context;
        }

        [HttpPost("GetToken")]
        [AllowAnonymous]
        public ActionResult<dynamic> GetToken(LoginRequestModel model)
        {
            var user=_context.Users.Where(x => x.Mail == model.Mail && x.Password == model.Password).FirstOrDefault();
            if (user==null)
            {
               return  BadRequest("Kullanıcı Bulunamadı Veya Hatalı Giriş");
            }

            var ss= new { token= GenerateToken.Generate(user) };
            return ss;
        }
        [HttpGet("CehckLogin")]
        
        public ActionResult<dynamic> checkLogin()
        {
            var user =  _context.Users.Where(x => x.Id == Guid.Parse(User.Identity.UserId())).FirstOrDefault();

            if (user == null)
            {
                return new { Success = false };
            }
            else
            {
                return new { Success = true };
            }
        }

    }



     public class LoginRequestModel
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}

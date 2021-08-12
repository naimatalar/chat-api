using chat.Entites.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly ChatContext _context;

        public ConfigurationController(ChatContext context)
        {
            _context = context;
        }

        [HttpPost("CreateEditWebSite")]
        public ActionResult<dynamic> GetToken(CreateEditWebSiteRequestModel model)
        {
            if (model.Id==null)
            {
                var data = new Entites.WebSites
                {
                    WebSiteName = model.Name

                };
                _context.WebSites.Add(data) ;
                return data;
            }
            var rdata = _context.WebSites.Where(x => x.Id == model.Id ).FirstOrDefault();
            if (rdata == null)
            {
                BadRequest("Kayıt Bulunamadı");
            }
            rdata.WebSiteName=model.Name;
            _context.Entry(rdata).State = EntityState.Modified;

            _context.SaveChanges();
            return rdata;
        }
        [HttpGet("DeleteWebSite/{id}")]
        public ActionResult<dynamic> DeleteWebSite(Guid Id)
        {
         
            var rdata = _context.WebSites.Where(x => x.Id == Id).FirstOrDefault();
            if (rdata == null)
            {
                BadRequest("Kayıt Bulunamadı");
            }
            _context.WebSites.Remove(rdata);
            _context.SaveChanges();
            return rdata;
        }
        [HttpPost("CreateEditUser")]
        public ActionResult<dynamic> CreateUser(CreateEditUserRequestModel model)
        {
            if (model.Id == null)
            {
                var data = new Entites.User
                {
                    Name = model.Name,
                    Mail=model.Mail,
                    Password=model.Password,

                };
                _context.Users.Add(data);
                return data;
            }
            var rdata = _context.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            if (rdata == null)
            {
                BadRequest("Kayıt Bulunamadı");
            }
            rdata.Name = model.Name;
            rdata.Mail = model.Mail;
            rdata.Password = model.Password;


            _context.Entry(rdata).State = EntityState.Modified;

            _context.SaveChanges();
            return rdata;
        }
        [HttpGet("DeleteUSer/{id}")]
        public ActionResult<dynamic> DeleteUser(Guid Id)
        {

            var rdata = _context.Users.Where(x => x.Id == Id).FirstOrDefault();
            if (rdata == null)
            {
                BadRequest("Kayıt Bulunamadı");
            }
            _context.Users.Remove(rdata);
            _context.SaveChanges();
            return rdata;
        }


    }





    public class CreateEditWebSiteRequestModel {
        public Guid? Id { get; set; }
        public string Name { get; set; }

    }

    public class CreateEditUserRequestModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }

    }
}


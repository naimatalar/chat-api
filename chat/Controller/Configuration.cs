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

        [HttpGet("GetAllWebSite")]
        public ActionResult<dynamic> getWebSite()
        {
            var d = _context.WebSites.Select(x => new
            {
                x.CreateDate,
                LastMessageDate = x.MessageTopics.FirstOrDefault() == null ? "-" : x.MessageTopics.FirstOrDefault().CreateDate.ToString("dd-MMM-yyyy hh:mm"),
                x.Id,
                x.WebSiteName,
                MessageTopicsCount = x.MessageTopics.Count(),

            }).ToList();
            return d;
        }
        [HttpGet("GetAllWebSiteById/{Id}")]
        public ActionResult<dynamic> getWebSiteById(Guid Id)
        {
            var d = _context.WebSites.Where(x => x.Id == Id).Select(x => new
            {
                x.CreateDate,
                x.Id,
                x.WebSiteName,

            }).ToList();
            return d;
        }
        [HttpPost("CreateEditWebSite")]
        public ActionResult<dynamic> GetToken(CreateEditWebSiteRequestModel model)
        {
            if (model.Id == null)
            {
                var data = new Entites.WebSites
                {
                    WebSiteName = model.Name

                };
                _context.WebSites.Add(data);
                _context.SaveChanges();
                return data;
            }
            var rdata = _context.WebSites.Where(x => x.Id == model.Id).FirstOrDefault();
            if (rdata == null)
            {
                BadRequest("Kayıt Bulunamadı");
            }
            rdata.WebSiteName = model.Name;
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
        [HttpGet("GetAllUser")]
        public ActionResult<dynamic> getUsers()
        {
            var d = _context.Users.Select(x => new
            {
                x.CreateDate,
                x.Id,
                x.Name,
                x.Mail,
                x.Password

            }).ToList();
            return d;
        }
        [HttpGet("GetUserById/{id}")]
        public ActionResult<dynamic> getUsersById(Guid Id)
        {
            var d = _context.Users.Where(x => x.Id == Id).Select(x => new
            {
                x.CreateDate,
                x.Id,
                x.Name,
                x.Mail,
                x.Password

            }).FirstOrDefault();
            return d;
        }

        [HttpPost("CreateEditUser")]
        public ActionResult<dynamic> CreateUser(CreateEditUserRequestModel model)
        {
            if (model.Id == null)
            {
                var data = new Entites.User
                {
                    Name = model.Name,
                    Mail = model.Mail,
                    Password = model.Password,

                };
                _context.Users.Add(data);
                _context.SaveChanges();
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
        [HttpGet("DeleteUser/{id}")]
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
        [HttpGet("GetTopicById/{id}")]
        public ActionResult<dynamic> getTopic(Guid id)
        {
            var d = _context.MessageTopics.Where(x=>x.WebSiteId==id).Select(x => new
            {
                WebSiteName = x.WebSite.WebSiteName,
                WebSiteId = x.WebSite.Id,
                x.RiderMail,
                x.RiderName,
                Date=x.CreateDate.ToString("dd-MMM-yyyy")

            }).ToList();
            return d;
        }

        [HttpGet("GetMessageById/{id}")]
        public ActionResult<dynamic> getMessage(Guid id)
        {
            var d = _context.MessageContents.Where(x => x.MessageTopicId == id).Select(x => new
            {
                x.IsRead,
                x.Content,
                x.IsCustomer,
                 Date= x.CreateDate.ToString("dd-MMM-yyyy")

            }).ToList();
            return d;
        }

    }





    public class CreateEditWebSiteRequestModel
    {
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


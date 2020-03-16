using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendencesController : ApiController
    {
        private ApplicationDbContext dbContext;

        public AttendencesController()
        {
            dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendenceDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (dbContext.Attendences.Any(a=>a.AttendeeId==userId && a.GigId==dto.GigId))
            {
                return BadRequest("Attendence already exists");
            }
            var attend = new Attendence
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            dbContext.Attendences.Add(attend);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}

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
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext dbContext;

        public FollowingsController()
        {
            dbContext = new ApplicationDbContext();
        }
        
        [HttpPost]

        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (dbContext.Followings.Any(f=>f.FollowerId==userId && f.FolloweeId==dto.FolloweeId))
            {
                return BadRequest("Following already exists");
            }
            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            dbContext.Followings.Add(following);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}

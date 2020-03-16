using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext db;
        public GigsController()
        {
            db = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = db.Attendences
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(a=>a.Artist)
                .Include(a=>a.Genre)
                .ToList();
            var viewModel = new GigsViewModel
            {
                UpcomigGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        [Authorize]
        // GET: Gigs
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = db.Genre.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = db.Genre.ToList();
                return View("Create", viewModel);
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId=viewModel.Genre,
                Venue = viewModel.Venue,
            };
            db.Gigs.Add(gig);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
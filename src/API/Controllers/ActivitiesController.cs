using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers 
{    
   [ApiVersion("1.0")]
    [ApiController]
    [Route("activity/v{apiVersion:apiVersion}/[controller]")]
    public class ActivitiesController : ControllerBase 
    {
        private readonly DataContext _context;
        public ActivitiesController (DataContext context)
         {
            _context = context;
         }

         [HttpGet]
         public async Task<ActionResult<List<Activity>>> GetActivities()
         {
             var activities = await _context.Activities.ToListAsync();
             return activities;
         } 

         [HttpGet("{Id}")]
         public async Task<ActionResult<Activity>> GetActivity(Guid Id)
         {
             var activity = await _context.Activities.FindAsync(Id);
             return activity;
         }
    }
}
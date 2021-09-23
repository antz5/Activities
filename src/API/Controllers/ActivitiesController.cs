using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers {
    [ApiVersion ("1.0")]
    [ApiController]
    [Route ("activity/v{apiVersion:apiVersion}/[controller]")]
    public class ActivitiesController : BaseApiController {       

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities () {
            var activities = await Mediator.Send(new List.Query());
            return activities;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Activity>> GetActivity (Guid id) {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] Activity activity)
        {
            return Ok(await Mediator.Send(new Create.Command{Activity = activity}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id,[FromBody]Activity activity){
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Activity = activity}));
        }
    }
}
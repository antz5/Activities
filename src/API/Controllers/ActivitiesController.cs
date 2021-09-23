using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
            var result = await Mediator.Send(new Details.Query{Id = id});
            if(result is null)
            {
                return new NotFoundResult();
            }
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] Activity activity)
        {
            return Ok(await Mediator.Send(new Create.Command{Activity = activity}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id,[FromBody]Activity activity){
            activity.Id = id;
            var result = await Mediator.Send(new Edit.Command{Activity = activity});
            if(result)
            return Ok(result);

            return new NotFoundResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
           var result = await Mediator.Send(new Delete.Command{Id = id});

           if(result)
            return new NoContentResult();

            return new NotFoundResult();
        }
    }
}
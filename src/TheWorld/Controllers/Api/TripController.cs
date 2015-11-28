using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using System.Net;
using TheWorld.ViewModels;
using AutoMapper;
using Microsoft.Framework.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<WorldRepository> _logger;
        private IWorldRepository _repository;

        public TripController(IWorldRepository repository, ILogger<WorldRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet("")]
        public JsonResult Get()
        {
            //  var results = Mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTripsWithStops());
            var results = _repository.GetAllTripsWithStops();
            return Json(results);
        }

        [HttpPost("")]   ///  wazne, [FromBody] próbuje rzutowac informacje przesłane np. w JSONie]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(vm);

                    // Save to data base
                    _logger.LogInformation("Attempting to save a new trip");

                    _repository.AddTrip(newTrip);
                    if(_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Failed", ModelState = ModelState });
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to Save to DB");
                Response.StatusCode = Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
        }
    }
}

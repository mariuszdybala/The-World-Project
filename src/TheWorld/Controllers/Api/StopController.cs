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
using TheWorld.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private CoordService _coordService;
        private ILogger _logger;
        private IWorldRepository _repository;

        public StopController(IWorldRepository repository, ILogger<StopController> logger,
            CoordService coordService)
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            //string tripName = "Us Trip";
            try
            {

                var decode = WebUtility.UrlDecode(tripName);

                var result = _repository.GetTripByName(decode);
                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(result.Stops.OrderBy(x => x.Order)));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to get stops for trips {tripName}", ex);
                    return Json("Error occured finding stops");
                }
        }

        [HttpPost("")]
        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var decode = WebUtility.UrlDecode(tripName);
                    //Map to Entity
                    var stop = Mapper.Map<Stop>(vm);

                    //Get Coordinate
                    var coordResult = await _coordService.Lookup(stop.Name);

                    if(!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordResult.Message);
                    }
                    stop.Latitude = coordResult.Lat;
                    stop.Longitude = coordResult.Long;

                    //Save DB
                    _repository.SaveStop(decode,stop);
                    if(_repository.SaveAll())
                    {
                        return Json("Stop is Saved");

                    }
                }
                return Json("Set valid property for Stop");
            }
            catch (Exception ex)
            {
                return Json(new { ServerCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }

        }
    }
}

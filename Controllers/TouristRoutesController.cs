using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeXiecheng.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeXiecheng.API.Controllers
{
    [Route("api/[controller]")] // api/touristroutes
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        
        public TouristRoutesController(ITouristRouteRepository touristRouteRepository)
        {
            _touristRouteRepository = touristRouteRepository;
        }

        [HttpGet]
        public IActionResult GetTouristRoutes()
        {
            var touristRoutesFromRepo = _touristRouteRepository.GetTouristRoutes();
            if (touristRoutesFromRepo == null && touristRoutesFromRepo.Count() <= 0)
            {
                return NotFound("There are no routes.");
            }
            return Ok(touristRoutesFromRepo);
        }

        //api/touristroutes/{touristRouteId}
        [HttpGet("{touristRouteId}")]
        public IActionResult GetTouristRouteById(Guid touristRouteId)
        {
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
            if (touristRouteFromRepo == null)
            {
                return NotFound($"The id {touristRouteId} does not exist.");
            }
            return Ok(touristRouteFromRepo);
        }
    }
}
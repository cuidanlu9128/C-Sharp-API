using AutoMapper;
using FakeXiecheng.API.Models;
using FakeXiecheng.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Controllers
{
    [Route("api/touristRoutes/{touristRouteId}/pictures")]
    [ApiController]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private IMapper _mapper;

        public TouristRoutePicturesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository ??
                throw new ArgumentNullException(nameof(touristRouteRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetPicturesListForTouristRoute(Guid touristRouteId)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("The routes do not exist.");
            }
            var picturesFromRepo = _touristRouteRepository.GetPicturesByTouristRouteId(touristRouteId);
            if (picturesFromRepo == null || picturesFromRepo.Count() <= 0)
            {
                return NotFound("The picture does not exist.");
            }
            return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(picturesFromRepo));
        }

        [HttpGet("{pictureId}")]
        public IActionResult GetPicture(Guid touristRouteId, int pictureId)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("The tourist route does not exist.");
            }

            var pictureFromRepo = _touristRouteRepository.GetPicture(pictureId);
            if (pictureFromRepo == null)
            {
                return NotFound("The pic does not exist.");
            }
            return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
        }
    }
}

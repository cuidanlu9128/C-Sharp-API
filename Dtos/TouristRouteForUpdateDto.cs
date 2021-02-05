
using System.ComponentModel.DataAnnotations;


namespace FakeXiecheng.API.Dtos
{
    public class TouristRouteForUpdateDto : TouristRouteForManipulationDto
    { 
        [Required(ErrorMessage ="Update")]
        [MaxLength(1500)]
        public override string Description { get; set; }
    }
}

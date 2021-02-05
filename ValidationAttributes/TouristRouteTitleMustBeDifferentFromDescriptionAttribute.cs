using FakeXiecheng.API.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeXiecheng.API.ValidationAttributes
{
    public class TouristRouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext
            )
        {
            var touristRouteDto = (TouristRouteForManipulationDto)(validationContext.ObjectInstance);
            if (touristRouteDto.Title == touristRouteDto.Description)
            {
                return new ValidationResult(
                    "The route name could not be the same with description",
                    new[] { "TouristRouteForCreationDto" }
                    );
            }
                return ValidationResult.Success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalTwin.Application.DTOs
{
    public class CreateMotorRequestDto
    {
        [Required(ErrorMessage = " The name is required")]
        [StringLength (100, ErrorMessage = "The name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "The model cannot be longer than 50 characters.")]
        public string Model { get; set; } = string.Empty;

        [Range(0.1, 500, ErrorMessage = "The temperature must be between 0.1 and 500 °C.")]
        public double? MaxTemperature { get; set; }

        [Range(0.1, 10000, ErrorMessage = "The RPM must be between 0.1 and 10000.")]
        public double? MaxRPM { get; set; }

        [Range(0.1, 20000, ErrorMessage = "The current must be positive.")]
        public double? NominalCurrent { get; set; }
    }
}

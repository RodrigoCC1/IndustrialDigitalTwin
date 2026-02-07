using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitalTwin.Application.DTOs
{
    public class UpdateMotorRequestDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Range(0.1, 500)]
        public double MaxTemperature { get; set; }

        [Range(0.1, 1000)]
        public double MaxRPM { get; set; }

        [Range(1, 20000)]
        public double NominalCurrent { get; set; }
    }
}

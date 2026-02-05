using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTwin.Application.DTOs
{
    public class CreateMotorRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;

        public double? MaxTemperature { get; set; }
        public double? MaxRPM { get; set; }
        public double? NominalCurrent { get; set; }
    }
}

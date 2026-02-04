using DigitalTwin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTwin.Domain.Interfaces
{
    public interface IMotorService
    {
        Task<Motor?> GetMotorByIDAsync(Guid id);
        Task<IEnumerable<Motor>> GetAllMotorsAsync();
        Task<Motor> CreateMotorAsync (string name, string model, double maxTemp = 155, double nominalCurrent = 12.5, double maxRPM = 1800);
        Task ProcessTelimetryAsync(Guid motorId, double currentTemp, double currentRPM, double vibrationVelocity);
    }
}

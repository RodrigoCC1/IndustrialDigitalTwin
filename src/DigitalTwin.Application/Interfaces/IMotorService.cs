using DigitalTwin.Domain.Entities;
using DigitalTwin.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTwin.Application.Interfaces
{
    public interface IMotorService
    {
        Task<Motor?> GetMotorByIDAsync(Guid id);
        Task<IEnumerable<Motor>> GetAllMotorsAsync();
        Task<Motor> CreateMotorAsync(CreateMotorRequestDto request);
        Task UpdateMotorAsync(UpdateMotorRequestDto motorDto);
        Task ProcessTelimetryAsync(Guid motorId, double currentTemp, double currentRPM, double vibrationVelocity);
    }
}

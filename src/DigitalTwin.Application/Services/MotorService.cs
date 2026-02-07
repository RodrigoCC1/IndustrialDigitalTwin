using DigitalTwin.Application.DTOs;
using DigitalTwin.Application.Interfaces;
using DigitalTwin.Domain.Entities;
using DigitalTwin.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTwin.Application.Services
{
    public class MotorService : IMotorService
    {
        private readonly IMotorRepository _motorRepository;
        public MotorService(IMotorRepository motorRepository)
        {
            _motorRepository = motorRepository;
        }
        public async Task<Motor> CreateMotorAsync(CreateMotorRequestDto request)
        {
            var maxTemp = request.MaxTemperature ?? 155.0;
            var current = request.NominalCurrent ?? 12.5;
            var rpm = request.MaxRPM ?? 1800.0;

            var newMotor = new Motor(request.Name, request.Model, maxTemp, rpm, current);
            await _motorRepository.AddMotorAsync(newMotor);
            return newMotor;
        }

        public async Task<IEnumerable<Motor>> GetAllMotorsAsync()
        {
            return await _motorRepository.GetAllAsync();
        }

        public async Task<Motor?> GetMotorByIDAsync(Guid id)
        {
            return await _motorRepository.GetByIdAsync(id);
        }

        public async Task ProcessTelimetryAsync(Guid motorId, double currentTemp, double currentRPM, double vibrationVelocity)
        {
            var motor = await _motorRepository.GetByIdAsync(motorId);

            if (motor == null)
            {
                throw new KeyNotFoundException($"Device with ID {motorId} not found.");
            }

            motor.UpdateTelemetry(currentTemp, currentRPM, vibrationVelocity);

            await _motorRepository.UpdateMotorAsync(motor);

        }

        public async Task UpdateMotorAsync(UpdateMotorRequestDto motorDto)
        {
            var existingMotor = await _motorRepository.GetByIdAsync(motorDto.Id);
            
            if (existingMotor == null)
            {
                throw new KeyNotFoundException($"Motor with ID {motorDto.Id} not found.");
            }

            existingMotor.UpdateDetails(motorDto.Name, motorDto.Model, motorDto.MaxTemperature, motorDto.MaxRPM, motorDto.NominalCurrent);
        }
    }
}

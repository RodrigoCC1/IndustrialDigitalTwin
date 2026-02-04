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
        public async Task<Motor> CreateMotorAsync(string name, string model, double maxTemp = 155, double nominalCurrent = 12.5, double maxRPM = 1800)
        {
            var newMotor = new Motor (name, model, 155, 12.5, 1800);
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
    }
}

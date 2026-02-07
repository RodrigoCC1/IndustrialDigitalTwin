using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTwin.Domain.Entities
{
    /// <summary>
    /// Represents a industrial motor within the digital twin system.
    /// Modeled based on Siemens SIMOTICS DS motor specifications.
    /// </summary>
    public class Motor
    {
        // Identification properties
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Model { get; private set; }

        // Physical specifications (Nameplate Data)
        /// <summary>
        /// Maximun operating temperature in Celsius before critical failure. 
        /// </sumary>
        public double MaxOperatingTemperatureCelsius { get; private set; }

        /// <summary>
        /// Rated speed in RPM (Revolutions Per Minute).
        /// </summary>
        public double MaxRPM { get; private set; }
        public double NominalCurrent { get; private set; }

        // Real-time Telemetry
        public double CurrentTemperature { get; private set; }
        public double CurrentRPM { get; private set; }

        /// <summary>
        /// Vibration velocity in mm/s (RMS). Used for predictive maintenance analysis.
        /// </summary>
        public double VibrationVelocity { get; private set; }
        public bool IsRunning { get; private set; }
        public DeviceStatus Status { get; private set; }

        protected Motor() { }

        /// <summary>
        /// Initializes a new instance of the Device class with standard industrial limits.
        /// </summary>
        /// <param name="name">Unique identifier or location tag (e.g., "Conveyor Belt Motor 01").</param>
        /// <param name="model">Manufacturer model designation.</param>
        /// <param name="maxTemp">Max operating temp (Default: 155.0 for Class F)</param>
        /// <param name="nominalCurrent">Rated Amps (Default: 12.5A for 10HP).</param>
        /// <param name="maxRPM">Rated RPM (Default: 1800 RPM for 4-pole motor).</param>
        public Motor(string name, string model, double maxTemp = 155, double nominalCurrent = 12.5, double maxRPM = 1800)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Model = model;
            MaxOperatingTemperatureCelsius = maxTemp;
            NominalCurrent = nominalCurrent;
            MaxRPM = maxRPM;

            // Initial state
            Status = DeviceStatus.Stopped;
            CurrentTemperature = 25.0; // Ambient temperature
            IsRunning = false;
        }

        public void TurnOn()
        {
            if (Status == DeviceStatus.CriticalError)
                throw new InvalidOperationException("Cannot turn on a motor in critical error state.");

            IsRunning = true;
            Status = DeviceStatus.Running;
        }

        public void TurnOff()
        {
            IsRunning = false;
            Status = DeviceStatus.Stopped;
        }

        /// <summary>
        /// Updates the device state with fresh sensor data and evaluates safety rules.
        /// </summary>
        public void UpdateTelemetry(double temperatureCelsius, double rpm, double vibrationVelocity)
        {
            CurrentTemperature = temperatureCelsius;
            CurrentRPM = rpm;
            VibrationVelocity = vibrationVelocity;

            // Safety and maintenance checks
            if (CurrentTemperature > MaxOperatingTemperatureCelsius)
            {
                Status = DeviceStatus.CriticalError;
                IsRunning = false; // Automatically stop the motor
            }
            else if (VibrationVelocity > 7.1) // ISO 10816 Zone C/D limit
            {
                Status = DeviceStatus.MaintenanceRequired;
            }
            else if (IsRunning && Status != DeviceStatus.CriticalError)
            {
                Status = DeviceStatus.Running;
            }
        }

        public void UpdateDetails(string name, string model, double maxTemp, double nominalCurrent, double maxRPM)
        {
            Name = name;
            Model = model;
            MaxOperatingTemperatureCelsius = maxTemp;
            NominalCurrent = nominalCurrent;
            MaxRPM = maxRPM;
        }
        public enum DeviceStatus
        {
            Stopped,
            Running,
            MaintenanceRequired,
            CriticalError
        }
    }
}

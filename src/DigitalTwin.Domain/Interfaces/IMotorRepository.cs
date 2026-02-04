using DigitalTwin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTwin.Domain.Interfaces
{
    public interface IMotorRepository
    {
        /// <summary>
        /// Retrieves a specific motor by its unique identifier.
        /// Returns null if not found.
        /// </summary>
        Task<Motor?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all registered motors in the system.
        /// </summary>
        Task<IEnumerable<Motor>> GetAllAsync();

        /// <summary>
        /// Persists a new motor into the storage.
        /// </summary>
        Task AddMotorAsync(Motor motor);

        /// <summary>
        /// Updates the state of an existing motor (e.g., new telemetry or status change).
        /// </summary>
        Task UpdateMotorAsync(Motor motor);

        /// <summary>
        /// Removes a motor from the system.
        /// </summary>
        Task DeleteMotorAsync(Guid id);
    }
}

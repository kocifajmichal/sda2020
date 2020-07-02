using ambulance_api.Models;

namespace ambulance_api.Services
{
    /// <summary>
    /// Interface for access to database operations
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        /// Get ambulance data by given Id
        /// </summary>
        /// <param name="ambulanceId">Ambulance unique Id</param>
        /// <returns>Ambulance data if found. If not found, returns null.</returns>
         Ambulance GetAmbulanceData(string ambulanceId);

        /// <summary>
        /// If ambulance with given Id does not exist, creates new one. If exists, updates the existing one.
        /// </summary>
        /// <param name="ambulanceId">Ambulance unique Id</param>
        /// <param name="ambulance">Ambulance data</param>
        /// <returns></returns>
         Ambulance UpsertAmbulanceData(string ambulanceId, Ambulance ambulance);

        /// <summary>
        /// Deletes the ambulance with given Id
        /// </summary>
        /// <param name="ambulanceId">Ambulance unique Id</param>
        /// <returns>True, if ambulance exists. False if does not exist.</returns>
         bool DeleteAmbulanceData(string ambulanceId);
    }
}
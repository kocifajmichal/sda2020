using System.Collections.Generic;
using System.IO;
using ambulance_api.Models;
using LiteDB;
using Microsoft.AspNetCore.Hosting;

namespace ambulance_api.Services
{
    public class DataRepository : IDataRepository
    {
        private readonly LiteDatabase myLiteDatabase;
        private const string AmbulancesCollectionName = "Ambulances";

        public DataRepository(IWebHostEnvironment hostingEnvironment)
        {
            string connectionString = Path.Combine(hostingEnvironment.ContentRootPath, 
                "data-repository.db");
            myLiteDatabase = new LiteDatabase(connectionString);
        }

        public bool DeleteAmbulanceData(string ambulanceId)
        {
            return GetCollection().Delete(ambulanceId);
        }

        public Ambulance GetAmbulanceData(string ambulanceId)
        {
            var ambulance = GetCollection().FindById(ambulanceId);
            if (ambulance != null)
            {
                ambulance.EstimateAndSortWaitingList();
            }
            return ambulance;
        }

        public Ambulance UpsertAmbulanceData(string ambulanceId, Ambulance ambulance)
        {
            ambulance.EstimateAndSortWaitingList();
            var existing = GetCollection().FindById(ambulanceId);
            if (existing == null)
            {
                ambulance.Id = ambulanceId;
                GetCollection().Insert(ambulance);
            }
            else
            {
                GetCollection().Update(ambulance);
            }
            return ambulance;
        }

        private ILiteCollection<Ambulance> GetCollection()
        {
            return myLiteDatabase.GetCollection<Ambulance>(AmbulancesCollectionName);
        }
    }
}
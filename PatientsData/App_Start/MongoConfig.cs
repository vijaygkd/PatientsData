using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using PatientsData.Models;

namespace PatientsData.App_Start
{
    public static class MongoConfig
    {
        public static void Seed()
        {
            var patients = PatientDb.Open();

            if (!patients.AsQueryable().Any(p => p.Name == "Scott"))
            {
                var data = new List<Patient>()
                {
                    new Patient
                    {
                        Name = "Scott",
                        Ailments = new List<Ailment>() {new Ailment {Name = "Crazy"}},
                        Medications = new List<Medication> {new Medication {Name = "Asprin", Doses = 2}}
                    },
                    new Patient
                    {
                        Name = "Joy",
                        Ailments = new List<Ailment>() {new Ailment {Name = "Crazy"}},
                        Medications = new List<Medication> {new Medication {Name = "Asprin", Doses = 2}}
                    },
                    new Patient
                    {
                        Name = "Sarah",
                        Ailments = new List<Ailment>() {new Ailment {Name = "Crazy"}},
                        Medications = new List<Medication> {new Medication {Name = "Asprin", Doses = 2}}
                    }
                };

                patients.InsertMany(data);
            }
        }
    }
}
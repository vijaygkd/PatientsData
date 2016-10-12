using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Driver;
using PatientsData.Models;

namespace PatientsData.Controllers
{
    public class PatientsController : ApiController
    {
        IMongoCollection<Patient> _patients;

        public PatientsController()
        {
            _patients = PatientDb.Open();
        }

        public IEnumerable<Patient> Get()
        {
            return _patients.FindSync(patient => true).ToList();
        }

        public HttpResponseMessage Get(string id)
        {
            var patient = _patients.FindSync(p => p.Id == id).First();
            if (patient == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found!");
            }
            return Request.CreateResponse(patient);
        }

        [Route("api/patients/{id}/medications")]
        public HttpResponseMessage GetMedications(string id)
        {
            var patient = _patients.FindSync(p => p.Id == id).First();
            if (patient == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found!");
            }
            return Request.CreateResponse(patient.Medications);
        }
    }
}

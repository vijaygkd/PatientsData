using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MongoDB.Driver;
using PatientsData.Models;

namespace PatientsData.Controllers
{
    [EnableCors("*", "*", "GET")]
    [Authorize]         // For api authentication
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

        public IHttpActionResult Get(string id)
        {
            var patient = _patients.FindSync(p => p.Id == id).First();
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [Route("api/patients/{id}/medications")]
        public IHttpActionResult GetMedications(string id)
        {
            var patient = _patients.FindSync(p => p.Id == id).First();
            if (patient == null)
            {
                return NotFound();
                    //Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found!");
            }
            return Ok(patient.Medications);     //Request.CreateResponse(patient.Medications);
        }
    }
}

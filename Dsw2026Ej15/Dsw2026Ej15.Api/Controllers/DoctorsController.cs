using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Controllers
{
    public class DoctorsController : AppController
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        // Desarrollo del Primer EndPoint: POST /doctors
        [HttpPost("doctors")]
        public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
        {
            //throw new Exception("Error de prueba");

            if (string.IsNullOrWhiteSpace(request.Name) || 
                string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest("Nombre y Matrícula son obligatorios.");
            }
            
            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality is null)
            {
                throw new ValidationException("Especialidad no encontrada.");
            }

            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
            _persistence.SaveDoctor(doctor);

            return Created();
        }

        // Desarrollo del Segundo EndPoint: GET /doctors
        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = _persistence
                .GetDoctors()
                .Where(d => d.IsActive)
                .Select(d => new
                {
                    d.Id,
                    d.Name,
                    d.LicenseNumber
                });

            return Ok(doctors);
        }

        // Desarrollo del Tercer EndPoint: GET /doctors/{id}
        [HttpGet("doctors/{id}")]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
            {
                throw new ValidationException("Médico no encontrado.");
            }

            return Ok(new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name
            });
        }

        //Desarrollo del Cuarto EndPoint: DELETE /doctors/{id}
        [HttpDelete("doctors/{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
            {
                throw new ValidationException("Médico no encontrado.");
            }

            doctor.Deactivate();

            return NoContent();
        }
    }
}

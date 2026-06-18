using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        //List<Doctor> GetDoctors();
        //Doctor GetDoctorById(Guid Id);
        //List<Speciality> GetSpecialities();
        Speciality? GetSpecialityById(Guid id);
        void SaveDoctor(Doctor doctor);
    }
}
using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private List<Doctor> doctors;

    private List<Speciality> specialities;

    public PersistenceInMemory()
    {
        doctors = new List<Doctor>();

        specialities = LoadSpecialities();
    }

    private List<Speciality> LoadSpecialities()
    {
        string json = File.ReadAllText("specialities.json");

        return JsonSerializer.Deserialize<List<Speciality>>(json)
               ?? new List<Speciality>();
    }

    public List<Doctor> GetDoctors()
    {
        return doctors;
    }

    public Doctor? GetDoctorById(Guid id)
    {
        return doctors.FirstOrDefault(d => d.Id == id);
    }

    public void AddDoctor(Doctor doctor)
    {
        doctors.Add(doctor);
    }

    public List<Speciality> GetSpecialities()
    {
        return specialities;
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return specialities.FirstOrDefault(s => s.Id == id);
    }
}
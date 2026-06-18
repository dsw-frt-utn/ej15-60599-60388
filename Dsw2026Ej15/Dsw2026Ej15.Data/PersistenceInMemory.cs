using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private List<Doctor> _doctors = [];

    private List<Speciality> _specialities = [];

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }

    private void LoadSpecialities()
    {
        try
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
            var json = File.ReadAllText(jsonPath);
            var specialities = JsonSerializer.Deserialize<List<Speciality>>(json,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
            _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
        }
        catch (Exception)
        {

        }
    }

    //public List<Doctor> GetDoctors()
    //{
    //    return doctors;
    //}

    //public Doctor? GetDoctorById(Guid id)
    //{
    //    return doctors.FirstOrDefault(d => d.Id == id);
    //}

    //public void AddDoctor(Doctor doctor)
    //{
    //    doctors.Add(doctor);
    //}

    //public List<Speciality> GetSpecialities()
    //{
    //    return specialities;
    //}

    //public Speciality? GetSpecialityById(Guid id)
    //{
    //    return specialities.FirstOrDefault(s => s.Id == id);
    //}
}
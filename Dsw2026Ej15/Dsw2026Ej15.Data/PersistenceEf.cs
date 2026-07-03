using Dsw2026Ej15.Data.Context;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly AppDbContext _context;

    public PersistenceEf(AppDbContext context)
    {
        _context = context;
    }

    public List<Doctor> GetDoctors()
    {
        return _context.Doctors
            .Include(d => d.Speciality)
            .ToList();
    }

    public Doctor? GetDoctorById(Guid id)
    {
        return _context.Doctors
            .Include(d => d.Speciality)
            .FirstOrDefault(d => d.Id == id);
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return _context.Specialities
            .FirstOrDefault(s => s.Id == id);
    }

    public void SaveDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CheckAppCore.Data;
using CheckAppCore.Models;

namespace CheckAppCore.Data
{
    public class DbInitializer
    {
        public static void Initialize(CheckAppContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.AppointmentTypes.Any())
            {
                return;   // DB has been seeded
            }

            var consultas = new []
            {
            new AppointmentType{Name = "Cardiologia"},
            new AppointmentType{Name = "Dermatologia"},
            new AppointmentType{Name = "Geriatria"},
            new AppointmentType{Name = "Ginecologia"},
            new AppointmentType{Name = "Neurologia"},
            new AppointmentType{Name = "Obstetrícia"},
            new AppointmentType{Name = "Ortopedia"},
            new AppointmentType{Name = "Otorrinolaringologia"},
            new AppointmentType{Name = "Pneumonologia"},
            new AppointmentType{Name = "Psiquiatria"},
            new AppointmentType{Name = "Urologia"},
            };

            foreach (var c in consultas)
            {
                context.AppointmentTypes.Add(c);
            }

            context.SaveChanges();
        }
    }
}

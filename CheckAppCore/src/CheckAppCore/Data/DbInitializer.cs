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

            var card = new AppointmentType { Name = "Cardiologia" };
            var derma = new AppointmentType { Name = "Dermatologia" };
            var ger = new AppointmentType { Name = "Geriatria" };
            var gineco = new AppointmentType { Name = "Ginecologia" };
            var neuro = new AppointmentType { Name = "Neurologia" };
            var obst = new AppointmentType { Name = "Obstetrícia" };
            var orto = new AppointmentType { Name = "Ortopedia" };
            var otorrino = new AppointmentType { Name = "Otorrinolaringologia" };
            var pneumo = new AppointmentType { Name = "Pneumonologia" };
            var psi = new AppointmentType { Name = "Psiquiatria" };
            var uro = new AppointmentType { Name = "Urologia" };

            if (!context.AppointmentTypes.Any())
            {
                var consultas = new[]
                {
                    card, derma,ger,gineco,neuro,obst,orto,otorrino,pneumo,psi,uro
                };

                foreach (var c in consultas)
                {
                    context.AppointmentTypes.Add(c);
                }

                context.SaveChanges();
            }
            
            if (!context.Professionals.Any())
            {
                var professionals = new[]
                {
                    new Professional { Name = "José Ricardo", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = card}, new ProfessionalAppointmentType() { AppointmentType = derma } } },
                    new Professional { Name = "Olegário Santiago", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = psi}, new ProfessionalAppointmentType() { AppointmentType = uro } } },
                    new Professional { Name = "Henrique Freitas", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = ger }, new ProfessionalAppointmentType() { AppointmentType = gineco } } },
                    new Professional { Name = "Doroteia Fernandes", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = gineco }, new ProfessionalAppointmentType() { AppointmentType = neuro } } },
                    new Professional { Name = "Camilo Nunes", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = neuro }, new ProfessionalAppointmentType() { AppointmentType = obst } } },
                    new Professional { Name = "Hipólito Matos", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = obst }, new ProfessionalAppointmentType() { AppointmentType = orto } } },
                    new Professional { Name = "Alcides Guerra", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = orto }, new ProfessionalAppointmentType() { AppointmentType = otorrino } } },
                    new Professional { Name = "Emanuel Da Costa", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = otorrino }, new ProfessionalAppointmentType() { AppointmentType = pneumo } } },
                    new Professional { Name = "Priscila Madeira", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = pneumo }, new ProfessionalAppointmentType() { AppointmentType = psi } } },
                    new Professional { Name = "Carlinhos Gouveia", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = psi}, new ProfessionalAppointmentType() { AppointmentType = uro } } },
                    new Professional { Name = "Matilde Serafim", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = uro }, new ProfessionalAppointmentType() { AppointmentType = card } } },
                    new Professional { Name = "Fábia Santana", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = card }, new ProfessionalAppointmentType() { AppointmentType = derma } } },
                    new Professional { Name = "Roldão Santos", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = derma }, new ProfessionalAppointmentType() { AppointmentType = ger } } },
                    new Professional { Name = "Teresa Magalhães", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = ger }, new ProfessionalAppointmentType() { AppointmentType = gineco } } },
                    new Professional { Name = "Tristão Pereira", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = gineco }, new ProfessionalAppointmentType() { AppointmentType = neuro } } },
                    new Professional { Name = "Custódio Barros", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = neuro }, new ProfessionalAppointmentType() { AppointmentType = obst } } },
                    new Professional { Name = "Otávio Alves", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = obst }, new ProfessionalAppointmentType() { AppointmentType = orto } } },
                    new Professional { Name = "Cristiano Madeira", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = orto }, new ProfessionalAppointmentType() { AppointmentType = otorrino } } },
                    new Professional { Name = "Antônio Belo", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = otorrino }, new ProfessionalAppointmentType() { AppointmentType = pneumo } } },
                    new Professional { Name = "Henrique Duarte", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = pneumo }, new ProfessionalAppointmentType() { AppointmentType = psi } } },
                    new Professional { Name = "Aloísio Rocha", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = psi}, new ProfessionalAppointmentType() { AppointmentType = uro } } },
                    new Professional { Name = "Lúcio Matos", NumeroCRM = "9999999", ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType>() { new ProfessionalAppointmentType() {AppointmentType = uro } } },
                };

                foreach (var c in professionals)
                {
                    context.Professionals.Add(c);
                }

                context.SaveChanges();
            }
        }
    }
}

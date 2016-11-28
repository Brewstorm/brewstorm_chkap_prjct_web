using System;
using CheckAppCore.Data;
using CheckAppCore.Models;
using System.Collections.Generic;

namespace CheckAppCore.Repositories
{
    public class ProfessionalRepository : EntityFrameworkRepository<CheckAppContext, Professional>
    {
        public ProfessionalRepository(CheckAppContext context) : base(context)
        {

        }

        public bool RegisterProfessional(RegisterModel registerModel)
        {
            try
            {
                var professional = new Professional
                {
                    NumeroCRM = "N/A",
                    Bairro = "N/A",
                    Endereco = "N/A",
                    ProfessionalAppointmentTypes = new List<ProfessionalAppointmentType> {
                    new ProfessionalAppointmentType { AppointmentTypeId = registerModel.AppointmentType }
                }
                };

                Context.Professionals.Add(professional);
                Context.SaveChanges();               

                return new AgendaRepository(Context).CreateDefaultAgendaByProfessional(professional); ;
            }
            catch
            {
                return false;
            }
        }
    }
}

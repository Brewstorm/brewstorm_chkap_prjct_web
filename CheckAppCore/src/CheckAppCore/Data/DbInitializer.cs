using System;
using System.Collections.Generic;
using System.Linq;
using CheckAppCore.Enumerators;
using CheckAppCore.Models;

namespace CheckAppCore.Data
{
    public class DbInitializer
    {
        public static void Initialize(CheckAppContext context)
        {
            context.Database.EnsureCreated();

            var card = new AppointmentType {Name = "Cardiologia", ProfessionalName = "Cardiologista"};
            var derma = new AppointmentType {Name = "Dermatologia", ProfessionalName = "Dermatologista"};
            var ger = new AppointmentType {Name = "Geriatria", ProfessionalName = "Geriatra"};
            var gineco = new AppointmentType {Name = "Ginecologia", ProfessionalName = "Ginecologista"};
            var neuro = new AppointmentType {Name = "Neurologia", ProfessionalName = "Neurologista"};
            var obst = new AppointmentType {Name = "Obstetrícia", ProfessionalName = "Obstetra"};
            var orto = new AppointmentType {Name = "Ortopedia", ProfessionalName = "Ortopedista"};
            var otorrino = new AppointmentType
            {
                Name = "Otorrinolaringologia",
                ProfessionalName = "Otorrinolaringologista"
            };
            var pneumo = new AppointmentType {Name = "Pneumonologia", ProfessionalName = "Pneumologista"};
            var psi = new AppointmentType {Name = "Psiquiatria", ProfessionalName = "Psiquiatra"};
            var uro = new AppointmentType {Name = "Urologia", ProfessionalName = "Urologista"};

            if (!context.AppointmentTypes.Any())
            {
                var consultas = new[]
                {
                    card, derma, ger, gineco, neuro, obst, orto, otorrino, pneumo, psi, uro
                };

                foreach (var c in consultas)
                {
                    context.AppointmentTypes.Add(c);
                }

                context.SaveChanges();
            }

            if (!context.PersonalInfo.Any())
            {
                var personalInfo = new[]
                {
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "José Ricardo",
                        SrcPhoto = "/images/professionals/masc_1.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "Henrique Freitas",
                        SrcPhoto = "/images/professionals/masc_2.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "Doroteia Fernandes",
                        SrcPhoto = "/images/professionals/fem_1.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "Hipólito Matos",
                        SrcPhoto = "/images/professionals/masc_3.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "Alcides Guerra",
                        SrcPhoto = "/images/professionals/masc_4.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Feminino,
                        Name = "Priscila Madeira",
                        SrcPhoto = "/images/professionals/fem_2.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "Carlos Gouveia",
                        SrcPhoto = "/images/professionals/masc_5.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Feminino,
                        Name = "Fábia Santana",
                        SrcPhoto = "/images/professionals/fem_3.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "Roldão Santos",
                        SrcPhoto = "/images/professionals/masc_6.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "Tristão Pereira",
                        SrcPhoto = "/images/professionals/masc_7.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Masculino,
                        Name = "Custódio Barros",
                        SrcPhoto = "/images/professionals/masc_8.jpg"
                    },
                    new PersonalInfo
                    {
                        GenderID = GenderEnum.Feminino,
                        Name = "Cristiane Madeira",
                        SrcPhoto = "/images/professionals/fem_4.jpg"
                    }
                };

                foreach (var c in personalInfo)
                {
                    context.PersonalInfo.Add(c);
                }

                context.SaveChanges();
            }

            if (!context.Professionals.Any())
            {
                var professionals = new[]
                {
                    new Professional
                    {
                        PersonalInfoID = 1,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = card},
                                new ProfessionalAppointmentType() {AppointmentType = derma}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 2,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = ger},
                                new ProfessionalAppointmentType() {AppointmentType = gineco}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 3,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = gineco},
                                new ProfessionalAppointmentType() {AppointmentType = neuro}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 4,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = obst},
                                new ProfessionalAppointmentType() {AppointmentType = orto}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 5,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = orto},
                                new ProfessionalAppointmentType() {AppointmentType = otorrino}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 6,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = pneumo},
                                new ProfessionalAppointmentType() {AppointmentType = psi}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 7,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = card},
                                new ProfessionalAppointmentType() {AppointmentType = derma}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 8,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = gineco},
                                new ProfessionalAppointmentType() {AppointmentType = neuro}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 9,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = neuro},
                                new ProfessionalAppointmentType() {AppointmentType = obst}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 10,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = orto},
                                new ProfessionalAppointmentType() {AppointmentType = otorrino}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 11,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = pneumo},
                                new ProfessionalAppointmentType() {AppointmentType = psi}
                            }
                    },
                    new Professional
                    {
                        PersonalInfoID = 12,
                        NumeroCRM = "9999999",
                        Endereco = "Rua Oswaldo Schmidt",
                        Bairro = "Guanabara",
                        ProfessionalAppointmentTypes =
                            new List<ProfessionalAppointmentType>()
                            {
                                new ProfessionalAppointmentType() {AppointmentType = uro}
                            }
                    },
                };

                foreach (var c in professionals)
                {
                    context.Professionals.Add(c);
                }

                context.SaveChanges();
            }

            if (!context.Agenda.Any())
            {
                var agendas = new[]
                {
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 1,
                        AppointmentTypeID = 1,
                        Value = 100,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 2,
                        AppointmentTypeID = 2,
                        Value = 80,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 3,
                        AppointmentTypeID = 3,
                        Value = 95,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 4,
                        AppointmentTypeID = 4,
                        Value = 70,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 5,
                        AppointmentTypeID = 5,
                        Value = 75,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 6,
                        AppointmentTypeID = 6,
                        Value = 95,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 7,
                        AppointmentTypeID = 7,
                        Value = 110,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 8,
                        AppointmentTypeID = 8,
                        Value = 99,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 9,
                        AppointmentTypeID = 9,
                        Value = 79,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 10,
                        AppointmentTypeID = 10,
                        Value = 85.5,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 11,
                        AppointmentTypeID = 11,
                        Value = 100,
                        Active = true
                    },
                    new Agenda()
                    {
                        Name = "Principal",
                        ProfessionalID = 12,
                        AppointmentTypeID = 10,
                        Value = 100,
                        Active = true
                    }
                };

                foreach (var a in agendas)
                {
                    context.Agenda.Add(a);
                }

                context.SaveChanges();

                if (!context.AgendaSchedules.Any())
                {
                    var asch = new[]
                    {
                        new AgendaSchedule()
                        {
                            AgendaID = 1,
                            Date = DateTime.Today,
                            BeginTime = 480,
                            EndTime = 1080,
                            IntervalTime = 30
                        }
                    };

                    var ae = new[]
                    {
                        new AgendaException() {AgendaScheduleID = 1, BeginTime = 720, EndTime = 780}
                    };

                    foreach (var a in asch)
                    {
                        context.AgendaSchedules.Add(a);
                    }

                    context.SaveChanges();

                    foreach (var a in ae)
                    {
                        context.AgendaExceptions.Add(a);
                    }

                    context.SaveChanges();
                }
            }

            if (!context.Users.Any())
            {
                var roles = new[]
                {
                    new Role() {Description = "Professional"},
                    new Role() {Description = "Patient"}
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();

                context.Users.Add(new User()
                {
                    EmailAddress = "ander.fel.meurer@gmail.com",
                    FirstName = "Anderson",
                    LastName = "Meurer",
                    FacebookID = "1169409783140925"
                });

                context.Users.Add(new User()
                {
                    EmailAddress = "ADMIN",
                    FirstName = "Usuário",
                    LastName = "Admin",
                    Password = "CA1234",
                    FacebookID = ""
                });

                context.SaveChanges();

                context.UsersRoles.Add(new UserRole() {RoleID = 2, UserID = 1});
                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Policy;
using DbFiller.Entities;
using AutoPoco.Engine;
using AutoPoco;
using AutoPoco.DataSources;

namespace DbFiller.Factories
{
    public class EntityGenerator
    {

        public static IList<Person> GenerateDBData(int numberOfRecords)
        {
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => { c.UseDefaultConventions(); });
                x.AddFromAssemblyContainingType<Person>();
                x.Include<Person>()
                   .Setup(c => c.Id).Use<IntegerIdSource>()
                   .Setup(c => c.FirstName).Use<FirstNameSource>()
                   .Setup(c => c.Surname).Use<LastNameSource>()
                   .Setup(c => c.BirthDate).Use<DateOfBirthSource>()
                   .Setup(c => c.Email).Use<EmailAddressSource>();
            });

            IGenerationSession session = factory.CreateSession();

            IList<Person> people = session.List<Person>(numberOfRecords).Impose(x => x.Created, DateTime.UtcNow).Get();

            return people;
        }
    }
}
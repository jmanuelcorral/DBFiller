using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbFiller.BulkInsertion;
using DbFiller.Entities;

namespace DbFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            String ConnectionString = @"server=.\SQLExpress;Integrated Security=true;database=examples;Connect Timeout=180;";
            String TableName = @"tblPersons";

            DatabaseBulkTools.Insert<Person>(Factories.EntityGenerator.GenerateDBData(10), ConnectionString, TableName);
            Console.ReadKey();
        }
    }
}

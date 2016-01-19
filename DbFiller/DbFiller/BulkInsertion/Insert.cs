using FastMember;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbFiller.BulkInsertion
{
    public class DatabaseBulkTools
    {
        public static void Insert<T>(IList<T> objectsToInsert, String ConnectionString, String TableName)
        {
            var table = new DataTable();
            using (var reader = ObjectReader.Create(objectsToInsert))
            {
                table.Load(reader);
            }

            using (SqlConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                using (SqlBulkCopy s = new SqlBulkCopy(dbConnection))
                {
                    s.DestinationTableName = TableName;

                    foreach (var column in table.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());

                    s.WriteToServer(table);
                }
            }

        }
    }
}
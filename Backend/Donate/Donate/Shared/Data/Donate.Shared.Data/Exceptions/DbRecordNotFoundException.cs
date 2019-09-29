using System;

namespace Donate.Shared.Data.Exceptions
{
    public class DbRecordNotFoundException : Exception
    {
        public DbRecordNotFoundException(string table, string id) : 
            base($"Unable to find record with ID {id} in table {table}")
        {

        }
    }
}
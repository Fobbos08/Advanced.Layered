using System;
using LiteDB;

namespace Shared
{
    public abstract class DbProvider
    {
        private readonly string dbName;

        public DbProvider(string databaseName)
        {
            dbName = databaseName;
        }

        public void Execute(Action<LiteDatabase> action)
        {
            using (var db = new LiteDatabase(dbName))
            {
                action.Invoke(db);
            }
        }
    }
}

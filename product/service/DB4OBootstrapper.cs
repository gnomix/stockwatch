using System;
using System.Configuration;
using System.IO;
using gorilla.utility;
using solidware.financials.service.orm;

namespace solidware.financials.service
{
    public class DB4OBootstrapper : Command
    {
        string database_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"mokhan.ca\momoney\default.db4o");

        Settings settings = new Settings(ConfigurationManager.AppSettings);

        public void run()
        {
            if (settings.named<bool>("reset.db"))
                if (File.Exists(database_path)) File.Delete(database_path);

            using (var database = new DB4OConnectionFactory().Open())
            {
                database.Store(new LastOpened(Clock.now()));
            }
        }
    }
}
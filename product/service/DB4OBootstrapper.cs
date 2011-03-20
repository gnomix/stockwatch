using System;
using System.Configuration;
using System.IO;
using Db4objects.Db4o;
using gorilla.utility;
using solidware.financials.service.orm;

namespace solidware.financials.service
{
    public class DB4OBootstrapper : Command
    {
        string database_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                                            @"mokhan.ca\momoney\default.db4o");

        Settings settings = new Settings(ConfigurationManager.AppSettings);

        public void run()
        {
            if (settings.named<bool>("reset.db"))
                if (File.Exists(database_path)) File.Delete(database_path);

            ensure_directories_exist();

            using (var database = Db4oEmbedded.OpenFile(database_path))
            {
                database.Store(new LastOpened(Clock.now()));
            }
        }

        void ensure_directories_exist()
        {
            var company_dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"mokhan.ca");
            if (!Directory.Exists(company_dir))
                Directory.CreateDirectory(company_dir);

            var application_dir = Path.Combine(company_dir, "momoney");
            if (!Directory.Exists(application_dir))
                Directory.CreateDirectory(application_dir);
        }
    }
}
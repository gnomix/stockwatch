using System;
using System.IO;
using Db4objects.Db4o;

namespace solidware.financials.service.orm
{
    public class DB4OConnectionFactory : ConnectionFactory
    {
        public DB4OConnectionFactory()
        {
            database_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"mokhan.ca\momoney\default.db4o");
        }

        public Connection Open()
        {
            ensure_directories_exist();
            return Open(database_path);
        }

        public Connection Open(string path)
        {
            if (null == connection)
            {
                connection = new DB4OConnection(Db4oFactory.OpenFile(database_path));
                connection.Ext().Configure().ActivationDepth(int.MaxValue);
                connection.Ext().Configure().UpdateDepth(int.MaxValue);
            }
            return connection;
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

        string database_path;
        DB4OConnection connection;
    }
}
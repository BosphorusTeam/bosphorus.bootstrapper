﻿using Bosphorus.BootStapper.Common;
using Bosphorus.Dao.NHibernate.Extension.Driver.OracleManaged;
using Bosphorus.Dao.NHibernate.Fluent.PersistenceConfigurerProvider;
using FluentNHibernate.Cfg.Db;

namespace Aras.Common.Customization.Bosphorus.Dao.NHibernate.Fluent.PersistenceConfigurerProvider
{
    internal class Test : AbstractPersistenceConfigurerProvider
    {
        private readonly Perspective perspective;

        public Test(Perspective perspective)
        {
            this.perspective = perspective;
        }

        protected override IPersistenceConfigurer GetPersistenceProvider()
        {
            const string dataSource = @"
                  (DESCRIPTION =
                    (ADDRESS = (PROTOCOL = TCP)(HOST = test-esas.araskargo.com.tr)(PORT = 1521))
                    (CONNECT_DATA =
                      (SERVER = DEDICATED)
                      (SERVICE_NAME = esastest)
                    )
                  )
            ";

            string connectionString = string.Format("DATA SOURCE={0};PERSIST SECURITY INFO=True;USER ID={1};Password={2};enlist=dynamic", dataSource, "ESASLIVE", "0racle");

            OracleDataClientConfiguration persistenceConfigurer = OracleDataClientConfiguration
                .Oracle10
                .Managed()
                .ConnectionString(connectionString);


            if (perspective == Perspective.Debug)
            {
                persistenceConfigurer = persistenceConfigurer.ShowSql().FormatSql();
            }

            return persistenceConfigurer;
        }
    }
}

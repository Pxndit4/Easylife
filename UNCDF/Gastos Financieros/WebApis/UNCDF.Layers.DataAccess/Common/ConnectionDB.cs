using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using UNCDF.Utilities;

namespace UNCDF.Layers.DataAccess
{
    public class ConnectionDB
    {
        public static string GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            var appSetting = root.GetSection("ConnectionSettings");

            string connectionString = string.Empty;

            var scb = new SqlConnectionStringBuilder();

            scb.InitialCatalog = appSetting["InitialCatalog"];

            scb.DataSource = UEncrypt.Decrypt(appSetting["DataSource"]);
            scb.UserID = UEncrypt.Decrypt(appSetting["UserID"]);
            scb.Password = UEncrypt.Decrypt(appSetting["Password"]);
            connectionString = scb.ToString();

            return connectionString;

        }
    }
}

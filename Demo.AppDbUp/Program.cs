﻿using DbUp;
using DbUp.Engine;
using Rabbit.Foundation.Data;
using System;
using System.Configuration;
using System.Reflection;

namespace Demo.AppDbUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DemoAppDb"].ConnectionString;

            TryCreateDatabase(connectionString);

            var result = PerformUpgrade(connectionString);

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
            }
        }

        static DatabaseUpgradeResult PerformUpgrade(string connectionString)
        {
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            return upgrader.PerformUpgrade();
        }

        static void TryCreateDatabase(string connectionString)
        {
            var worker = new SqlServerDbWorker();

            if (worker.GetDatabaseId(connectionString) > 0)
            {
                Console.WriteLine("Database existed!");
            }
            else
            {
                Console.WriteLine("Database: creating...");

                worker.CreateAssociatedDatabase(connectionString);

                Console.WriteLine("Database creation: done.");
            }
        }
    }
}

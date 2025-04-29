using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace StandSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Random r = new Random();
            while (true)
            {
                
                //Thread.Sleep(1000);
                using (var connection = new SQLiteConnection("Data Source=C:\\workspace\\Stand.db"))
                {
                    connection.Open();
                    InsertCommand(connection,r);
                    if (connection != null)
                    {
                        Console.WriteLine("Connection");
                    }
                    else Console.WriteLine("No connection");
                    
                    
                }
            }
        }
        #region select
            public static void SelectCommand(SQLiteConnection connection)
            {
                string query = "SELECT *,MAX(TIME) FROM ParametersMeasure group by BlockID";

                // Создаем команду
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Выполняем запрос и получаем данные
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Читаем результаты построчно
                        while (reader.Read())
                        {
                            // Выводим значения всех столбцов
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader[i] + "\t");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        #endregion
        #region insert
        public static void InsertCommand(SQLiteConnection connection, Random r)
        {
            for (int i = 1; i <= 4; i++)
            {
                Thread.Sleep(r.Next(100,250));
                string query = $"insert into ParametersMeasure (BlockID,VoltageValue,ActiveLoadPower,ReactiveLoadPower,FullLoadPower, Time) values ({i}, {GetRandom(r, 100, 200)},{GetRandom(r, 100, 200)},{GetRandom(r, 100, 200)},{GetRandom(r, 100, 200)}, '{DateTime.Now.ToString("HH.mm.ss.ffff")}')";
            // Создаем команду
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("pisya");
                }
            }            
        }
        #endregion
        #region random
        public static string GetRandom(Random r, double min, double max)
        {
            double random = r.NextDouble() * (max - min) + min;
            return Math.Round(random,3).ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
        #endregion
    }
}

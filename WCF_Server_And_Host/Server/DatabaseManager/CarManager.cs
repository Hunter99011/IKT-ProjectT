using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Server.DatabaseManager
{
    public class CarManager : BaseDatabaseManager, ISQL
    {
        public List<Record> Select()
        {
            List<Record> records = new List<Record>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "SELECT * FROM mock_data ORDER BY id";
            try
            {
                MySqlConnection connection = BaseDatabaseManager.connection;
                connection.Open();
                command.Connection = connection;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Car oneCar = new Car();
                    oneCar.ID = int.Parse(reader["id"].ToString());
                    oneCar.Make = reader["CarMake"].ToString();
                    oneCar.Model = reader["CarModel"].ToString();
                    oneCar.Year = int.Parse(reader["CarYear"].ToString());
                    oneCar.Color = reader["Color"].ToString();
                    oneCar.Vin = reader["CarVin"].ToString();
                    records.Add(oneCar);
                }
            }
            catch (Exception ex)
            {
                Car oneCar = new Car();
                oneCar.Make = ex.Message;
                records.Add(oneCar);
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return records;
        }
        public List<Record> SelectSpec(string name)
        {
            List<Record> records = new List<Record>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = $"SELECT * FROM mock_data WHERE CarMake='{name}' ORDER BY id";
            try
            {
                MySqlConnection connection = BaseDatabaseManager.connection;
                connection.Open();
                command.Connection = connection;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Car oneCar = new Car();
                    oneCar.ID = int.Parse(reader["id"].ToString());
                    oneCar.Make = reader["CarMake"].ToString();
                    oneCar.Model = reader["CarModel"].ToString();
                    oneCar.Year = int.Parse(reader["CarYear"].ToString());
                    oneCar.Color = reader["Color"].ToString();
                    oneCar.Vin = reader["CarVin"].ToString();
                    records.Add(oneCar);
                }
            }
            catch (Exception ex)
            {
                Car oneCar = new Car();
                oneCar.Make = ex.Message;
                records.Add(oneCar);
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return records;
        }

        public int Insert(Record record)
        {
            Car car = record as Car;
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"INSERT INTO mock_data (CarMake,CarModel,CarYear,Color,CarVin) VALUES (@make,@model,@year,@color,@vin);";
            command.Parameters.Add(new MySqlParameter("@make", MySqlDbType.VarChar)).Value = car.Make;
            command.Parameters.Add(new MySqlParameter("@model", MySqlDbType.VarChar)).Value = car.Model;
            command.Parameters.Add(new MySqlParameter("@year", MySqlDbType.VarChar)).Value = car.Year;
            command.Parameters.Add(new MySqlParameter("@color", MySqlDbType.VarChar)).Value = car.Color;
            command.Parameters.Add(new MySqlParameter("@vin", MySqlDbType.VarChar)).Value = car.Vin;
            int hozzaAdottSorokSzama = 0;
            command.Connection = BaseDatabaseManager.connection;
            try
            {
                command.Connection.Open();
                try
                {
                    hozzaAdottSorokSzama=command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nem tudta hozzáadni!");
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nem tudta megnyitni!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                command.Connection.Close();
            }
            command.Parameters.Clear();
            return hozzaAdottSorokSzama;
        }

        public int Delete(int id)
        {
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"DELETE FROM mock_data WHERE id=@id";
            command.Parameters.Add(new MySqlParameter("id", id));
            command.Connection = BaseDatabaseManager.connection;
            int toroltSorokSzama = 0;
            try
            {
                command.Connection.Open();
                try
                {
                    toroltSorokSzama = command.ExecuteNonQuery();
                }
                catch
                {
                    return -1;
                }
            }
            catch
            {
                return -2;
            }
            finally
            {
                command.Connection.Close();
            }
            return toroltSorokSzama;
        }
        public int Update(Record record)
        {
            Car car = record as Car;
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"UPDATE mock_data SET CarMake=@make,CarModel=@model,CarYear=@year,Color=@color,CarVin=@vin WHERE id=@id";
            command.Parameters.Add(new MySqlParameter("id", car.ID));
            command.Parameters.Add(new MySqlParameter("make", car.Make));
            command.Parameters.Add(new MySqlParameter("model", car.Model));
            command.Parameters.Add(new MySqlParameter("year", car.Year));
            command.Parameters.Add(new MySqlParameter("color", car.Color));
            command.Parameters.Add(new MySqlParameter("vin", car.Vin));
            int modositottSorokSzama = 0;
            command.Connection = BaseDatabaseManager.connection;
            try
            {
                command.Connection.Open();
                try
                {
                    modositottSorokSzama = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nem tudta módosítani!");
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nem tudta megnyitni!");
                Console.WriteLine(ex.Message);
                return -2;
            }
            finally
            {
                command.Connection.Close();
            }
            command.Parameters.Clear();
            return modositottSorokSzama;
        }
    }
}
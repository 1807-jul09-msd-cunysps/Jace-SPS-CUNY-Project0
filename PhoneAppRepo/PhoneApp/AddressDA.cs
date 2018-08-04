using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace PhoneApp
{
    public static class AddressDA
    {
        static string conString = "Data Source=revature-cuny-jace-server.database.windows.net;Initial Catalog=Week2DB;Persist Security Info=True;User ID=jacewill963;Password=Khunta96#";
        static SqlConnection connect;
        static string command;
        static SqlCommand sqlCommand;
        static SqlDataReader data;

        public static List<Address> AddList = new List<Address>();


        public static void read()
        {
            //List<Country> countries = new List<Country>();
            using (connect = new SqlConnection(conString))
            {
                connect.Open();
                AddList.Clear();
                command = "SELECT * FROM Address";
                sqlCommand = new SqlCommand(command, connect);
                try
                {
                    //List<Address> addList = new List<Address>();
                    data = sqlCommand.ExecuteReader();
                    while (data.Read())
                    {
                        Address address = null;
                        foreach (var item in CountryDA.CtryList)
                        {
                            if (item.CountryID == (int)data[5])
                            {
                                address = new Address(
                                    (string)data[1],
                                    (string)data[2],
                                    (string)data[3],
                                    (int)data[4],
                                    (int)data[5],
                                    item);
                                break;
                            }
                        }
                        address.AddressID = (int)data[0];

                        AddList.Add(address);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR DATA ACCESS - READ_ADDRESS: " + ex.Message);
                    Console.ReadLine();
                    throw;
                }
            }
        }

        public static void insert(Address a)
        {
            //Create Query Statment using Parmeterized SQL
            command = $"INSERT INTO Address (ADDRESS, CITY, STATE, ZIP_CODE, COUNTRY_ID) " +
                      $"VALUES (@address, @city, @state, @zip, @countryID)";
            //Establish a New Connection
            using (connect = new SqlConnection(conString))
            {
                connect.Open();
                //Establish an Adapter to write the data
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    try
                    {
                        //Establish an Command using SQLCommand
                        using (sqlCommand = new SqlCommand(command, connect))
                        {
                            //Execute Query on the database 
                            sqlCommand.Parameters.AddWithValue("@address", a.AddressST);
                            sqlCommand.Parameters.AddWithValue("@city", a.City);
                            sqlCommand.Parameters.AddWithValue("@state", a.State);
                            sqlCommand.Parameters.AddWithValue("@zip", a.ZipCode);
                            sqlCommand.Parameters.AddWithValue("@countryID", a.CountryID);
                            sqlCommand.ExecuteNonQuery();
                        }

                        command = "SELECT MAX(ADDRESS_ID) FROM ADDRESS";
                        using (sqlCommand = new SqlCommand(command, connect))
                        {
                            using (data = sqlCommand.ExecuteReader())
                            {
                                while (data.Read()) { a.AddressID = (int)data[0]; }
                            }
                        }
                        AddList.Add(a);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR DATA ACCESS - INSERT_ADDRESS: " + ex.Message);
                        throw;
                    }
                    finally
                    {
                        connect.Close();
                    }
                };
            }

            /*List<Country> srchResults = new List<Country>();
            using (connect = new SqlConnection(conString))
            {
                connect.Open();
                using (sqlCommand = new SqlCommand(searchStr))
                {

                }
            }*/
        }

        public static void update(string updateCol, Contact updateElement, string updateResult)
        {
            command = $"UPDATE dbo.Address " +
            $"SET {updateCol} = @{updateCol} " +
            $"WHERE ADDRESS_ID = {updateElement.Address.AddressID}";
            try
            {
                using (connect = new SqlConnection(conString))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                    {
                        connect.Open();
                        using (sqlCommand = new SqlCommand(command, connect))
                        {

                            if (updateCol == "ZIP_CODE" || updateCol == "COUNTRY_ID")
                            {
                                sqlCommand.Parameters.AddWithValue($"@{updateCol}", Convert.ToInt32(updateResult));
                            }
                            else
                            {
                                sqlCommand.Parameters.AddWithValue($"@{updateCol}", updateResult);
                            }
                            sqlCommand.ExecuteNonQuery();
                        }
                        connect.Close();
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR DATA ACCESS - UPDATE_ADDRESS: " + ex.Message);
                throw;
            }
            
        }
    }
}

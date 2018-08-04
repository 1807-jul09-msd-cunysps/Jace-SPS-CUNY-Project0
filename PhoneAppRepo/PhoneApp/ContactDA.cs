using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace PhoneApp
{
    public static class ContactDA
    {
        static string conString = "Data Source=revature-cuny-jace-server.database.windows.net;Initial Catalog=Week2DB;Persist Security Info=True;User ID=jacewill963;Password=Khunta96#";
        static SqlConnection connect;
        //static string command;
        static SqlCommand sqlCommand;
        static SqlDataReader data;

        static List<Contact> CtactList = new List<Contact>();

        static public List<Contact> read()
        {
            CountryDA.read();
            AddressDA.read();
            using (connect = new SqlConnection(conString))
            {
                connect.Open();
                CtactList.Clear();
                string command = "SELECT * FROM Contacts";
                sqlCommand = new SqlCommand(command, connect);
                try
                {
                    //List<Contact> conList = new List<Contact>();
                    data = sqlCommand.ExecuteReader();
                    while (data.Read())
                    {
                        Contact contact = null;
                        foreach (var item in AddressDA.AddList)
                        {
                            if (item.AddressID == (int)data[5])
                            {
                                contact = new Contact(
                                    (string)data[1],
                                    (string)data[2],
                                    (int)data[3],
                                    (string)data[4],
                                    (int)data[5],
                                    (long)data[6],
                                    item);
                            }
                        }
                        if (contact == null) { contact = new Contact(); }
                        contact.ContactId = (int)data[0];
                        CtactList.Add(contact);
                    }
                    return CtactList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR DATA ACCESS - READ_CONTACTS: " + ex.Message);
                    Console.ReadLine();
                    throw;
                }
            }
        }

        public static void insert(Contact c)
        {
            //Create Query Statment using Parmeterized SQL
            string command = $"INSERT INTO dbo.Contacts (FIRST_NAME, LAST_NAME, AGE, GENDER, ADDRESS_ID, PHONE_NUMBER) " +
                      $"VALUES (@firstName, @lastName, @age, @gender, @addressID, @phoneNumber)";
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
                            sqlCommand.Parameters.AddWithValue("@firstName", c.FName);
                            sqlCommand.Parameters.AddWithValue("@lastName", c.LName);
                            sqlCommand.Parameters.AddWithValue("@age", c.Age);
                            sqlCommand.Parameters.AddWithValue("@gender", c.Gender);
                            sqlCommand.Parameters.AddWithValue("@addressID", c.AddressID);
                            sqlCommand.Parameters.AddWithValue("@phoneNumber", c.PhoneNumber);
                            sqlCommand.ExecuteNonQuery();
                        }
                        command = "SELECT MAX(CONTACT_ID) FROM CONTACTS";
                        using (sqlCommand = new SqlCommand(command, connect))
                        {
                            using (data = sqlCommand.ExecuteReader())
                            {
                                while (data.Read()) { c.ContactId = (int)data[0]; }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR DATA ACCESS - READ_CONTACTS: " + ex.Message);
                        throw;
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
            string command;
            if (updateCol == "COUNTRY_NAME" || updateCol == "COUNTRY_CODE")
            {
                if(updateCol == "COUNTRY_NAME")
                {
                    CountryDA.update(updateElement, updateResult, updateElement.Address.Country.CountryCode);
                }
                else
                {
                    CountryDA.update(updateElement, updateElement.Address.Country.CountryName, Convert.ToInt32(updateResult));
                }
                AddressDA.update("COUNTRY_ID", updateElement, Convert.ToString(updateElement.Address.CountryID));
            }
            else if (updateCol == "ADDRESS" || updateCol == "CITY" || updateCol == "STATE" || updateCol == "ZIP_CODE")
            {
                AddressDA.update(updateCol, updateElement, updateResult);
            }
            else
            {
                command = $"UPDATE dbo.Contacts " +
                $"SET {updateCol} = @{updateCol} " +
                $"WHERE CONTACT_ID = {updateElement.ContactId}";

                try
                {
                    using (connect = new SqlConnection(conString))
                    {
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                        {
                            connect.Open();
                            using (sqlCommand = new SqlCommand(command, connect))
                            {

                                if (updateCol.Equals("AGE") || updateCol == "ZIP_CODE" || updateCol == "COUNTRY_CODE")
                                {
                                    sqlCommand.Parameters.AddWithValue($"@{updateCol}", Convert.ToInt32(updateResult));
                                }
                                else if (updateCol == "PHONE_NUMBER")
                                {
                                    sqlCommand.Parameters.AddWithValue($"@{updateCol}", Convert.ToInt64(updateResult));
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
                    Console.WriteLine("ERROR DATA ACCESS - UPDATE_CONTACTS: " + ex.Message);
                    throw;
                }
            }
        }

        public static void delete(Contact delContact)
        {
            string command = $"DELETE FROM dbo.Contacts " +
                             $"WHERE CONTACT_ID = @contactID";
            using (connect = new SqlConnection(conString))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    connect.Open();
                    using (sqlCommand = new SqlCommand(command, connect))
                    {
                        sqlCommand.Parameters.AddWithValue("@contactID", delContact.ContactId);
                        sqlCommand.ExecuteNonQuery();
                    }
                    connect.Close();
                };
            }
            CtactList.Remove(delContact);
        }
    }
}

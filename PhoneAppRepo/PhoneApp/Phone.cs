using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PhoneApp
{
    class Phone
    {
        //Attributes
        string phoneName;
        string fname;
        string lname;
        string email;
        long phoneNumber;
        public string PhoneName { get => phoneName; set => phoneName = value; }
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string Email { get => email; set => email = value; }
        public long PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        internal List<Contact> contactList;


        //Constructors
        public Phone() { }
        public Phone(string phoneName, string fname, string lname, string email, long phoneNumber)
        {
            this.PhoneName = phoneName;
            this.Fname = fname;
            this.Lname = lname;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        public bool read()
        {
            string conString = "Data Source=revature-cuny-jace-server.database.windows.net;Initial Catalog=Week2DB;Persist Security Info=True;User ID=jacewill963;Password=Khunta96#";
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                string command = "SELECT * FROM PHONE";
                System.Console.WriteLine(this.phoneName);
                SqlCommand sqlCommand = new SqlCommand(command, connect);
                try
                {
                    SqlDataReader data = sqlCommand.ExecuteReader();
                    while (data.Read())
                    {
                        this.PhoneName = (string)data[0];
                        this.Fname = (string)data[1];
                        this.Lname = (string)data[2];
                        this.PhoneNumber = (long)data[3];
                        this.Email = (string)data[4];
                    }
                    return (this.phoneName != null) ? false : true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR DATA ACCESS - INSERT_ADDRESS: " + ex.Message);
                    throw;
                }

            }

        }

        public void insert()
        {
            //Create Query Statment using Parmeterized SQL
            string conString = "Data Source=revature-cuny-jace-server.database.windows.net;Initial Catalog=Week2DB;Persist Security Info=True;User ID=jacewill963;Password=Khunta96#";
            string command = $"INSERT INTO Phone " +
                              "VALUES (@phoneName, @firstName, @lastName, @phoneNumber, @email);";
            //Establish a New Connection
            using (SqlConnection connect = new SqlConnection(conString))
            {
                connect.Open();
                //Establish an Adapter to write the data
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    try
                    {
                        //Establish an Command using SQLCommand
                        using (SqlCommand sqlCommand = new SqlCommand(command, connect))
                        {
                            //Execute Query on the database 
                            sqlCommand.Parameters.AddWithValue("@phoneName", this.PhoneName);
                            sqlCommand.Parameters.AddWithValue("@firstName", this.Fname);
                            sqlCommand.Parameters.AddWithValue("@lastName", this.Lname);
                            sqlCommand.Parameters.AddWithValue("@phoneNumber", this.PhoneNumber);
                            sqlCommand.Parameters.AddWithValue("@email", this.Email);
                            sqlCommand.ExecuteNonQuery();
                        }
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
        }
    }
}

using System;

using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
namespace PhoneApp
{
    class Program
	{
		static void Main(string[] args)
		{
            //SqlConnection connect = null;
            try
            {
                Phone myPhone = new Phone();
                bool phoneCheck = myPhone.read();
                
                //Check if phone data exists
                if (phoneCheck)
                {
                    //if data doesn't exist ask use for phone information
                    Console.WriteLine(":::::New Phone Creation:::::");
                    Console.WriteLine();

                    Console.WriteLine("First Name*:");
                    myPhone.Fname = Console.ReadLine();
                    myPhone.PhoneName = myPhone.Fname + "'s Phone";

                    Console.WriteLine("Last Name (Optional):");
                    myPhone.Lname = Console.ReadLine();

                    Console.WriteLine("Email (Optional):");
                    myPhone.Email = Console.ReadLine();

                    Console.WriteLine("Phone Number*:");
                    myPhone.PhoneNumber = Convert.ToInt64(Console.ReadLine());

                    //insert into database using ExecuteNonQuery().
                    myPhone.insert();
                }
                //Declare lists to store Addresses and Countries

                //Load Contacts
                myPhone.contactList = ContactDA.read();

                //ConnectToDB(myPhone, AddList, CtryList, "READ_CONTACTS");
                
                //Displa Main menu
                bool display = true;
                string currDisplay = "main";
                string mainMenu = ":::::Main Menu:::::" + System.Environment.NewLine + "(1) Manage Contacts" + System.Environment.NewLine + "(2) Phone Logs" + System.Environment.NewLine + "(3) Factory Reset" + System.Environment.NewLine + "(4) Turn Off" + System.Environment.NewLine + ":::::Main Menu:::::";
                string currMenu = mainMenu;
                string menuSel;
                bool redisplay = false;
                int result;

                while (display == true && currDisplay != null)
                {
                    Console.Clear();
                    Console.WriteLine(currMenu);
                    //Console.WriteLine(currDisplay);
                    menuSel = Console.ReadLine();
                    if (redisplay)
                    {
                        switch (menuSel)
                        {
                            case "1":
                                currMenu = mainMenu;
                                currDisplay = "main";
                                break;
                            case "2":
                                display = false;
                                break;
                            default:
                                break;
                        }
                        redisplay = false;
                        continue;
                    }

                    switch (currDisplay)
                    {
                        case "main":
                            {
                                switch (menuSel)
                                {
                                    case "1":
                                        currMenu = ":::::Manage Contacts:::::" + System.Environment.NewLine +
                                                   "(1) View Contacts" + System.Environment.NewLine +
                                                   "(2) Add Contacts" + System.Environment.NewLine +
                                                   "(3) Update Contact" + System.Environment.NewLine +
                                                   "(4) Delete Contact" + System.Environment.NewLine +
                                                   "(5) Cancel" + System.Environment.NewLine +
                                                   ":::::Manage Contacts:::::";
                                        currDisplay = "contacts";
                                        continue;
                                    case "2":
                                        currMenu = ":::::Phone Logs:::::" + System.Environment.NewLine +
                                                   "(1) View Phone Logs" + System.Environment.NewLine +
                                                   "(2) Delete Phone Log Entry" + System.Environment.NewLine +
                                                   "(3) Clear Phone Logs" + System.Environment.NewLine +
                                                   "(4) Cancel" + System.Environment.NewLine +
                                                   ":::::Phone Logs:::::";
                                        currDisplay = "phonelogs";
                                        continue;
                                    case "3":
                                        currMenu = ":::::Factory Reset:::::" + System.Environment.NewLine + 
                                                   "(1) Reset Phone to Factory Settings" + System.Environment.NewLine + 
                                                   "(2) Cancel" + System.Environment.NewLine + 
                                                   ":::::Factory Reset:::::";
                                        currDisplay = "reset";
                                        continue;
                                    case "4":
                                        currMenu = ":::::Shut Down:::::" + System.Environment.NewLine + 
                                                   "(1) Are you sure you want to shut down?" + System.Environment.NewLine + 
                                                   "(2) Cancel" + System.Environment.NewLine + 
                                                   ":::::Shut Down:::::";
                                        continue;
                                    default:
                                        currMenu += "\n \nSelection entered wasn't valid, please select options 1-4.";
                                        continue;
                                }
                            }
                        case "contacts":
                            {
                                /*
                                 *":::::Manage Contacts:::::" + System.Environment.NewLine + 
                                    "(1) View Contacts" + System.Environment.NewLine + 
                                    "(2) Add Contacts" + System.Environment.NewLine + 
                                    "(3) Update Contact" + System.Environment.NewLine +
                                    "(4) Delete Contact" + System.Environment.NewLine + 
                                    "(5) Cancel" + System.Environment.NewLine + 
                                    ":::::Manage Contacts:::::"; 
                                 */
                                bool reDisplayList;
                                switch (menuSel)
                                {
                                    case "1":// Update Contact
                                        currMenu = ":::::View Contact::::: \n(1) View All Contacts \n(2) Search Contacts \n(3) Cancel \n:::::View Contact:::::";
                                        currDisplay = "view-contacts";
                                        continue;
                                    case "2"://Add Contact
                                        Console.Clear();
                                        Console.WriteLine(":::::Add Contact:::::");
                                        Console.WriteLine();

                                        string fname, lname, gender, email, state, city, countryName, addressSt;
                                        int age, zip, countryCode;
                                        long phoneNum;
                                        Console.WriteLine("First Name:");
                                        fname = Console.ReadLine();

                                        Console.WriteLine("Last Name:");
                                        lname = Console.ReadLine();

                                        Console.WriteLine("Gender (Male , Female):");
                                        gender = Console.ReadLine();

                                        Console.WriteLine("Email:");
                                        email = Console.ReadLine();

                                        Console.WriteLine("Phone Number:");
                                        phoneNum = Convert.ToInt64(Console.ReadLine());

                                        Console.WriteLine("Address:");
                                        addressSt = Console.ReadLine();

                                        Console.WriteLine("State:");
                                        state = Console.ReadLine();

                                        Console.WriteLine("City:");
                                        city = Console.ReadLine();

                                        Console.WriteLine("Age:");
                                        age = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Zip Code:");
                                        zip = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Country:");
                                        countryName = Console.ReadLine();

                                        Console.WriteLine("Country Code:");
                                        countryCode = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Save Contact?: (1) Save, (2) Cancel");
                                        result = Convert.ToInt16(Console.ReadLine());
                                        if (result == 1)
                                        {
                                            //connect.Open();
                                            Country c = null;
                                            bool storeData = true;
                                            foreach (var item in CountryDA.CtryList)
                                            {
                                                if(Convert.ToString(item.CountryName).ToLower() == countryName.ToLower())
                                                {
                                                    c = item;
                                                    storeData = false;
                                                }
                                            }
                                            if (storeData)
                                            {
                                                c = new Country();
                                                c.CountryName = countryName;
                                                c.CountryCode = countryCode;
                                                //===============================================
                                                //DATA ACCESS METHOD
                                                CountryDA.insert(c);
                                                //===============================================
                                            }
                                            Address a = new Address(addressSt, city, state, zip, c.CountryID, c);
                                            AddressDA.insert(a);
                                            Contact con = new Contact(fname, lname, age, gender, email, a.AddressID, phoneNum, a);
                                            ContactDA.insert(con);
                                            myPhone.contactList.Add(con);
                                            currMenu = mainMenu + "\n \n Contact Added!";
                                            currDisplay = "main";
                                            redisplay = true;
                                        }
                                        else
                                        {
                                            currMenu = mainMenu;
                                            currDisplay = "main";
                                        }

                                        continue;
                                    case "3"://Update Contacts
                                        int contactID = 0;
                                        if (myPhone.contactList.Count == 0)
                                        {
                                            currMenu = mainMenu + "There are no Contacts to Update.\n";
                                            currDisplay = "main";
                                            continue;
                                        }
                                        Console.Clear();
                                        currMenu = ":::::Update Contact::::: \n Search for a contact to edit by First name or Last name";
                                        reDisplayList = true;
                                        while (reDisplayList)
                                        {
                                            Console.Clear();
                                            Console.WriteLine(currMenu);
                                            string search = Console.ReadLine();
                                            var searchQuery =
                                                from elements in myPhone.contactList
                                                where search.Equals(elements.FName, StringComparison.InvariantCultureIgnoreCase) ||
                                                      search.Equals(elements.LName, StringComparison.InvariantCultureIgnoreCase)
                                                select elements;
                                            if (searchQuery.Count() == 0) {
                                                currMenu += "\n \nThe Search returned no results please try again"; continue;
                                            }

                                            foreach (var contact in searchQuery)
                                            {
                                                currMenu += System.Environment.NewLine + contact.displayContact();
                                            }
                                            reDisplayList = false;
                                        }
                                        Console.Clear();
                                        reDisplayList = true;
                                        while (reDisplayList)
                                        {
                                        currMenu += "Please enter the Contact ID of the contact to update.";
                                        Console.WriteLine(currMenu);
                                        if (int.TryParse(Console.ReadLine(), out contactID))
                                            {
                                                Console.WriteLine(contactID);
                                                reDisplayList = false;
                                                break;
                                            }
                                         else
                                            {
                                                currMenu += "\nPlease enter only numeric values for Contact ID.\n";
                                                continue;
                                            }
                                        }
                                        currMenu += "\n \nWhat would you like to update? Press (12) to Cancel \n(1) First Name \n(2) Last Name \n(3) Gender \n(4) Address \n(5) State \n(6) City \n(7) Country \n(8) Age \n(9) Zip Code \n(10) Country Code \n(11) Phone Number";
                                        var updateCol = "";
                                        var updateElement = new Contact();
                                        foreach (var item in myPhone.contactList)
                                        {
                                            if (item.ContactId == contactID)
                                            {
                                                updateElement = item;
                                                break;
                                            }
                                        }
                                        reDisplayList = true;
                                        while (reDisplayList)
                                        {
                                            Console.Clear();
                                            Console.WriteLine(currMenu);
                                            string sel = Console.ReadLine();
                                            string updateResult = "";
                                            switch (sel)
                                            {
                                                case "1":
                                                    Console.Write("First Name: ");
                                                    updateCol = "FIRST_NAME";
                                                    
                                                    break;
                                                case "2":
                                                    Console.Write("Last Name: ");
                                                    updateCol = "LAST_NAME";
                                                    break;
                                                case "3":
                                                    Console.Write("Gender: ");
                                                    updateCol = "GENDER";
                                                    break;
                                                case "4":
                                                    Console.Write("Addres: ");
                                                    updateCol = "ADDRESS";
                                                    break;
                                                case "5":
                                                    Console.Write("State: ");
                                                    updateCol = "STATE";
                                                    break;
                                                case "6":
                                                    Console.Write("City: ");
                                                    updateCol = "CITY";
                                                    break;
                                                case "7":
                                                    Console.Write("Country: ");
                                                    updateCol = "COUNTRY_NAME";
                                                    break;
                                                case "8":
                                                    Console.Write("Age: ");
                                                    updateCol = "AGE";
                                                    break;
                                                case "9":
                                                    Console.Write("Zip Code: ");
                                                    updateCol = "ZIP_CODE";
                                                    break;
                                                case "10":
                                                    Console.Write("Country Code: ");
                                                    updateCol = "COUNTRY_CODE";
                                                    break;
                                                case "11":
                                                    Console.Write("Phone Number: ");
                                                    updateCol = "PHONE_NUMBER";
                                                    break;
                                                case "12":
                                                    currMenu = mainMenu;
                                                    currDisplay = "main";
                                                    reDisplayList = false;
                                                    continue;
                                                default:
                                                    currMenu += "\n \nSelection entered wasn't valid, please select options 1-11 or 12 to Cancel";
                                                    continue;
                                            }
                                            updateResult = Console.ReadLine();                                           

                                            ContactDA.update(updateCol, updateElement, updateResult);

                                            myPhone.contactList = ContactDA.read();

                                            Console.WriteLine("Contact Successfully Updated! Press (1) to continue Press (2) to Cancel");
                                            string continueUpdate = Console.ReadLine();
                                            if (continueUpdate == "1")
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                reDisplayList = false;
                                                currMenu = mainMenu + "\nContact Successfully Updated!";
                                                currDisplay = "main";
                                                continue;
                                            }
                                        }
                                        break;
                                    case "4"://Delete Contact
                                        if (myPhone.contactList.Count == 0)
                                        {
                                            currMenu = mainMenu + "There are no Contacts to Delete.\n";
                                            currDisplay = "main";
                                            continue;
                                        }
                                        redisplay = true;
                                        while (redisplay)
                                        {
                                            currMenu = ":::::Delete Contact:::::\n";
                                            foreach (Contact contact in myPhone.contactList)
                                            {
                                                currMenu += contact.displayContact() + System.Environment.NewLine;
                                            }
                                            currMenu += "\nSelect a record to Delete by Contact ID:";

                                            reDisplayList = true;
                                            Contact delContact = new Contact();
                                            while (reDisplayList)
                                            {
                                                Console.Clear();
                                                Console.WriteLine(currMenu);
                                                if (int.TryParse(Console.ReadLine(), out contactID))
                                                {
                                                    foreach (var contact in myPhone.contactList)
                                                    {
                                                        if (contact.ContactId == contactID)
                                                        {
                                                            delContact = contact;
                                                            reDisplayList = false;
                                                            break;
                                                        }
                                                    }
                                                    currMenu += "\nThat Contact ID wasn't found please try again.\n";
                                                    continue;
                                                }
                                                else
                                                {
                                                    currMenu += "\nPlease enter only numeric values for Contact ID.\n";
                                                    continue;
                                                }
                                            }
                                            ContactDA.delete(delContact);
                                            myPhone.contactList.Remove(delContact);
                                            redisplay = true;
                                            while (redisplay)
                                            {
                                                Console.WriteLine("Contact Deleted Press (1) to Continue (2) to Cancel");
                                                if (int.TryParse(Console.ReadLine(), out result))
                                                {  
                                                   if (result == 1)
                                                    {
                                                        break;
                                                    }
                                                    else if(result == 2)
                                                    {
                                                        currMenu = mainMenu;
                                                        currDisplay = "main";
                                                        redisplay = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nInvalid Entry try again.\n");
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    currMenu += "\nPlease enter only numeric values for Contact ID.\n";
                                                    continue;
                                                }
                                            }
                                        }
                                        break;
                                    case "5":
                                        currMenu = mainMenu;
                                        currDisplay = "main";
                                        continue;
                                    default:
                                        currMenu += "\n \nSelection entered wasn't valid, please select options 1-5.";
                                        continue;
                                }
                                continue;
                            }
                        case "view-contacts":
                            {
                                switch (menuSel)
                                {
                                    case "1":
                                        if (myPhone.contactList.Count == 0)
                                        {
                                            currMenu += System.Environment.NewLine + "No Contacts Saved to Display";
                                            currDisplay = "main";
                                            continue;
                                        }
                                        Console.Clear();
                                        currMenu = ":::::View All Contacts::::: \n";
                                        foreach (Contact contact in myPhone.contactList)
                                        {
                                            currMenu += contact.displayContact() + System.Environment.NewLine;
                                        }
                                        currMenu += "Press (1) to return to Main Menu, Press (2) to close app.";
                                        redisplay = true;
                                        continue;
                                    case "2":
                                        Console.WriteLine("Search contacts.");
                                        string search = Console.ReadLine();
                                        var searchQuery =
                                            from elements in myPhone.contactList
                                            where search.Equals(elements.FName, StringComparison.InvariantCultureIgnoreCase) ||
                                                  search.Equals(elements.LName, StringComparison.InvariantCultureIgnoreCase)
                                            select elements;
                                        currMenu = ":::::Search Contacts::::: \n";
                                        foreach (var contact in searchQuery)
                                        {
                                            currMenu += contact.displayContact() + System.Environment.NewLine;
                                        }
                                        currMenu += "Press (1) to return to Main Menu, Press (2) to close app.";
                                        redisplay = true;
                                        continue;
                                    case "3":
                                        currMenu = mainMenu;
                                        currDisplay = "main";
                                        continue;
                                    default:
                                        break;
                                }
                                break;
                            }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
		}
	}

}

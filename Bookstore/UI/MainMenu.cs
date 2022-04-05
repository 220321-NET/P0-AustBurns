using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Models;
using BL;
using DL;


namespace UI;


public class MainMenu 
{
    private readonly ISLBL _b1;

    public MainMenu(ISLBL b1)
    {
        _b1 = b1;
    }

    public void Start()
    {
            Console.WriteLine(" ____________________/|");
            Console.WriteLine(" |                  | |");
            Console.WriteLine(" |    Bookstore     | |");
            Console.WriteLine(" |       ____       | |");
            Console.WriteLine(" |       |  |       | |");
            Console.WriteLine(" |       |  |       |  ");

        bool exit = false;
        do
        {
            Console.WriteLine("Welcome to the Bookstore");
            Console.WriteLine("What would you like to do today?\n");
            Console.WriteLine("[1] Create a new account");
            Console.WriteLine("[2] Login to an existing account");
            Console.WriteLine("[x] Exit");
            Console.Write(":");
            string? info = Console.ReadLine();

            switch(info)
            {
                case "1":
                    newCustomer();  
                    break;

                case "2":
                    Login();
                    break;

                case "secret password": AdminAccount(); break;

                case "x":
                    Console.WriteLine("Goodbye!");
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

        }while(!exit);
    }


    public void newCustomer()
    {
        EnterUserData:

        Console.WriteLine("-------New User-------  "); 
        Console.WriteLine("Create a new Username:"); 
        string? username = Console.ReadLine();  //store this value inside of the customers table

        Customer customerToCreate = new Customer();
        
        try
        {
            customerToCreate.Username = username!;
        }
        catch(ValidationException ex)
        {
            Console.WriteLine(ex.Message);
            //goto EnterUserData;
        }
        catch(DuplicateWaitObjectException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("User Already Exists! ");
            //goto EnterUserData;
        }

        Customer createdCustomer = _b1.CreateCustomer(customerToCreate);
        if (createdCustomer != null)
            Console.WriteLine("\nYour account has been created");
    }


    public void Login()
    {
        EnterLogin:
        Console.Write("Enter Your username :");
        string? username = Console.ReadLine();

        Customer login = new Customer();

        try
        {
            login.Username = username;
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            goto EnterLogin;
        }

        int results = _b1.CheckLogin(login);
        switch(results)
        {
            case 1:
                Log1:
                Console.WriteLine("This username doesnt exist");
                Console.WriteLine("Press Enter To Try Again.");
                string? response = Console.ReadLine();
                if (response == null)
                    goto EnterLogin;
                else
                {
                    Console.WriteLine("Invalid Input");
                    goto Log1;
                }
                
            case 0:
                LoginMenu();
                break;
        }
    }

    private void LoginMenu()
    {
        Console.Write("Username: ");
        string? username = Console.ReadLine();
            bool exit2 = false;
            Console.WriteLine("\n Thank You for Logging in " + username );
            do
            {
                Console.WriteLine(" What Would you Like to do Now \n");
                Console.WriteLine("[1] Browse all Books");
                Console.WriteLine("[2] Search for Existing Book");
                Console.WriteLine("[3] Make a Request for a Book we Don't Have");
                Console.WriteLine("[4] View Your Purchase History");
                Console.WriteLine("[x] Go Back");
                string? input = Console.ReadLine();
                switch(input)
                {
                    case "1": BrowseAllBooks(); break;
                    case "2": SearchBooks(); break;
                    case "3":
                        Console.WriteLine("What is the Book that you would like to see us have?");
                        string? request = Console.ReadLine();
                        Console.WriteLine("We will try our best to add this to our collection! \n");
                        // Console.WriteLine("\n");
                        break;
                    case "4": UserHistory(); break;
                    case "x": exit2 = true; break;
                    default:  Console.WriteLine("Invalid Input"); break;
                }
            }while(!exit2);
    }


    private void AdminAccount()
    {
        bool exit3 = false;
        do
        {
            // pull the inventory from the data table once its connected by using SELECT * FROM Inventory

            Console.WriteLine("\nWhich Store would you like to manage?\n");
            Console.WriteLine("[1] Nashville, TN");
            Console.WriteLine("[2] Richmond, VA");
            Console.WriteLine("[x] go back");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Write("Nashville's Inventory:\nWhich ProductID would you like to update?  :");
                    string? NashProductID = Console.ReadLine();
                    Console.Write("What would you lke the new stock to be?  :");
                    string? newQuantityNash = Console.ReadLine();
                    //UPDATE NashvilleStore SET Quantity_Inv = newQuantityNash
                    break;
                case "2":
                    Console.Write("Richmond's Inventory:\nWhich ProductID would you like to update?  :");
                    string? RichProductID = Console.ReadLine();
                    Console.Write("What would you lke the new stock to be?  :");
                    string? newQuantityRich = Console.ReadLine();
                    //UPDATE RichmondStore SET Quantity_Inv = newQuantityRich
                    break;
                case "x":
                    exit3 = true;
                    break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
        } while (!exit3);
    }


    private void BrowseAllBooks()
{   bool exit4 = false;
    do 
    {
        // pull the inventory from the data table once its connected by using SELECT * FROM Inventory
        
        Console.WriteLine("\nWhich Store would you like to browse?\n");  
        Console.WriteLine("[1] Nashville, TN");
        Console.WriteLine("[2] Richmond, VA"); 
        Console.WriteLine("[x] go back");
        string? input = Console.ReadLine();
            switch(input)
            {
                case "1": Console.WriteLine("View Nashville's Inventory"); //SELECT * FROM NashvilleStore
                        break;                                             //Have it Display the Product info like Title: Content: Cost:
                case "2": Console.WriteLine("View Richmond's Inventory");//SELECT * FROM RichmondStore
                        break;
                case "x": exit4 = true; 
                        break;
                default:  Console.WriteLine("Invalid Input"); 
                        break;
            }
    }while(!exit4);
    
    
}

private void UserHistory()
    {
        //Link this to the account or customerID of the user
        Console.WriteLine("Here is the records we have on file\n "); //display all purchases a user has in their table
    }


private void SearchBooks()
{ bool exit5 = false;
    do 
    {
        // pull the inventory from the data table once its connected by using SELECT * FROM Inventory
        

            Console.WriteLine("\nWhich Store would you like to browse?\n");  
            Console.WriteLine("[1] Nashville, TN");
            Console.WriteLine("[2] Richmond, VA"); 
            Console.WriteLine("[x] go back");
            string? input = Console.ReadLine();
                switch(input)
                {
                    case "1": Console.WriteLine("View Nashville's Inventory");
                            bool exit6 = false;
                            do 
                            {
                                Console.WriteLine("What would yo like to search by?\n");  
                                Console.WriteLine("[1] Title");
                                Console.WriteLine("[2] Content"); 
                                Console.WriteLine("[x] go back");
                                string? info = Console.ReadLine();
                                    switch(info)
                                    {
                                        case "1": Console.WriteLine("Titles that match"); //use somthing like SELECT Title FROM Inventory WHERE Title LIKE '% Blah Blah%'
                                                break;
                                        case "2": Console.WriteLine("Contents that match");//use somthing like SELECT Content FROM Inventory WHERE Content LIKE '% Blah Blah%'
                                                break;
                                        case "x": exit6 = true; 
                                                break;
                                        default:  Console.WriteLine("Invalid Input"); 
                                                break;
                                    }
                            }while(!exit6);break;  
                                            case "2": Console.WriteLine("View Richmond's Inventory");
                                                    bool exit7 = false;
                            do 
                            {                                
                                Console.WriteLine("What would yo like to search by?\n");  
                                Console.WriteLine("[1] Title");
                                Console.WriteLine("[2] Content"); 
                                Console.WriteLine("[x] go back");
                                string? info = Console.ReadLine();
                                    switch(info)
                                    {
                                        case "1": Console.WriteLine("Titles that match"); 
                                                break;
                                        case "2": Console.WriteLine("Contents that match");
                                                break;
                                        case "x": exit7 = true; 
                                                break;
                                        default:  Console.WriteLine("Invalid Input"); 
                                                break;
                                    }
                            }while(!exit7);
                            break;
                    case "x": exit5 = true; 
                            break;
                    default:  Console.WriteLine("Invalid Input"); 
                            break;
                }
    }while(!exit5);
    
    
    
    //user the LIKE feature in sql to search through the table of books in Inventory
    
}

}
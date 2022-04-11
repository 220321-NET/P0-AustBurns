using System.ComponentModel.DataAnnotations;
using BL;
using DL;


namespace UI;


public class MainMenu 
{
    private readonly ISLBL _bl;

    public MainMenu(ISLBL bl)
    {
        _bl = bl;
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
    public void FillerLine()
    {
        Console.WriteLine("\n<<<<<<<<<<<<<<>>>>>>>>>>>>>>\n");
    }


    //Customer Information New, Login, Menu after successful login
    public void newCustomer()
    {
        FillerLine();

        Console.WriteLine("-------New User-------  "); 
        Console.WriteLine("Create a new Username:"); 
        string? username = Console.ReadLine();  

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

        Customer createdCustomer = _bl.CreateCustomer(customerToCreate);
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

        int results = _bl.CheckLogin(login);
        switch(results)
        {
            case 0:
                Log1:
                Console.WriteLine("This username doesnt exist");
                Console.WriteLine("Press R to retry.");
                string? loginResponse = Console.ReadLine();
                if (loginResponse.Trim().ToUpper()[0] == 'R')
                goto EnterLogin;
                else
                {
                    Console.WriteLine("Invalid Input");
                    goto Log1;
                }
                
            case 1:
                LoginMenu(_bl.SelectCustomer(login));
                break;
        }
    }
    public void LoginMenu(Customer currentCustomer)
    {
        FillerLine();
            bool exit2 = false;
            Console.Write($"Thank You for Logging in {currentCustomer.Username}" );
            do
            {
                LoginMenu:
                Console.WriteLine("\nWhat Would you Like to do Now? \n");
                Console.WriteLine("[1] Shop all Books");
                Console.WriteLine("[2] Make a Request for a Book we Don't Have");
                Console.WriteLine("[3] View Order History");
                Console.WriteLine("[x] Go Back");
                string? input = Console.ReadLine();
                switch(input)
                {
                    case "1": BrowseStoreBooks(currentCustomer); break;
                    case "2":
                        Console.WriteLine("What is the Book that you would like to see us have?");
                        string? request = Console.ReadLine();
                        Console.WriteLine($"We will try our best to add {request} to our collection! \n");
                        break;
                    case "3": CustomerHistory();  break;
                    case "x": exit2 = true; break;
                    default:  Console.WriteLine("Invalid Input"); break;
                }
            }while(!exit2);
    }
    public void CustomerHistory()
    {
        Console.WriteLine("These are all of Your Purchases.");
        // List<Order> getHistory = _bl.GetCustomerHistory();

        // if (getHistory.Count == 0)
        //     Console.WriteLine("You Haven't Made any Purchases!");
        // Input:
        // for (int i = 0; i < getHistory.Count; i++)       //runs through the list of stores until there arent any left
        // Console.WriteLine(getHistory[i].ToString());
    }

    //Admin privileges View current stock, add stock, add store, add product
    public void AdminAccount()
    {
        bool exit3 = false;
        do
        {
            // pull the inventory from the data table once its connected by using SELECT * FROM Inventory
            FillerLine();

            Console.WriteLine("Welcome Admin\nWhat would you like to do?\n");
            Console.WriteLine("[1] Update Product Stock ");
            Console.WriteLine("[2] View Current Stock");
            Console.WriteLine("[x] go back");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("UpdateStock()");
                    break;
                case "2":
                    AdminMenu();
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
    public void AdminMenu()
    {
        Console.WriteLine("Which Store would you like to manage?\n");  
        StoreFront shopIn = SelectStore();

        //continueToShop:
        Console.Write($"You have selected {shopIn}.");
        Product shopProduct = SelectInventory(shopIn);
    }
    public StoreFront? AdminStores()
    {
        Console.WriteLine("These are the store's in the Franchise.");
        List<StoreFront> allStores = _bl.SelectStore();

        if (allStores.Count == 0)
            return null;
        Input:
        for (int i = 0; i < allStores.Count; i++)       //runs through the list of stores until there arent any left
        Console.WriteLine(allStores[i].ToString());
        
        int choose;

        if (Int32.TryParse(Console.ReadLine(), out choose) && ((choose-1) >= 0 && (choose-1) < allStores.Count))
            return allStores[choose-1];
        else
        {
            Console.WriteLine("Input not accepted");
            goto Input;
        }
    }
    public Product AdminInventory(StoreFront getInv)
    {
        Console.WriteLine($"This is the inventory for the store you selected :");
            List<Product> codeProduct = _bl.GetInventory(getInv);

            if (codeProduct.Count == 0)
            return null;

            BadInput:
            for (int i = 0; i < codeProduct.Count; i++)
                Console.WriteLine($"[{i}]: \nTitle: {codeProduct[i].title} \nContent: {codeProduct[i].content}\nSelling for: ${codeProduct[i].cost}\n");//This list off the title of the books inside the list

            int productSelect;

            if(Int32.TryParse(Console.ReadLine(), out productSelect) && ((productSelect) >= 0 && (productSelect) < codeProduct.Count)) 
                return codeProduct[productSelect];  //This is a limit to where the input must fall on a number in between 0 and the amount of inventory
            else
            {
                Console.WriteLine("That item is not in this inventory, Try again");
                goto BadInput;
            }
    }
    public void UpdateStock()
    {

    }


    //The user interface, gets their input on which store they want to shop in and the inventory from that store
    public void BrowseStoreBooks(Customer currentCustomer)//define so that it stores which user is using the interface
    {
        
        Order currentOrder = new Order();
        int count = 0;

        FillerLine();

        Console.WriteLine("Which Store would you like to browse?\n");  
        StoreFront shopIn = SelectStore();

        continueToShop:
        Console.WriteLine("Pick through the library and find the books you would like.");
        Product shopProduct = SelectInventory(shopIn);

        Invalid:
        Console.WriteLine($"Would you like to buy {shopProduct.title} for {shopProduct.cost}? "); 
        Console.WriteLine("Yes or No");

        string? shopInput = Console.ReadLine().Trim().ToUpper();

        switch(shopInput[0])
        {
            case 'Y': 
                Console.WriteLine("\nItem was Added to Your Cart,");
                AddBookToCart(currentCustomer, shopIn, shopProduct, currentOrder, count); count++; 
                break;
            case 'N': Console.WriteLine("Item was not added"); break;
            default: Console.WriteLine("Invalid Input"); goto Invalid;
            break;

        }

        if (count > 0)
        {
            Console.WriteLine("Would you like to keep shopping?");
            Console.WriteLine("[1]: Continue Shoping");
            Console.WriteLine("[2]: Proceed to Checkout");
            Console.WriteLine("[x]: Go Back");
            string? checkoutInput = Console.ReadLine();

            switch(checkoutInput[0])
            {
                case '1':
                    goto continueToShop;
                case '2':
                    Checkout(currentOrder);
                    break;
                case 'X':
                    break;
            }
        }
    }
    public StoreFront? SelectStore()
    {   
        // pull the inventory from the data table once its connected by using SELECT * FROM Inventory
        //put into the DB
        Console.WriteLine("These are the store's we have currently.");
        List<StoreFront> allStores = _bl.SelectStore();

        if (allStores.Count == 0)
            return null;
        Input:
        for (int i = 0; i < allStores.Count; i++)       //runs through the list of stores until there arent any left
        Console.WriteLine(allStores[i].ToString());
        
        int choose;

        if (Int32.TryParse(Console.ReadLine(), out choose) && ((choose-1) >= 0 && (choose-1) < allStores.Count))
            return allStores[choose-1];
        else
        {
            Console.WriteLine("Input not accepted");
            goto Input;
        }
    
}
    public Product SelectInventory(StoreFront getInv)
    {
        FillerLine();
            Console.WriteLine($"This is the inventory :\n");
            List<Product> codeProduct = _bl.GetInventory(getInv);

            if (codeProduct.Count == 0)
            return null;

            BadInput:
            for (int i = 0; i < codeProduct.Count; i++)
                Console.WriteLine($"[{i}]: \nTitle: {codeProduct[i].title} \nContent: {codeProduct[i].content}\nSelling for: ${codeProduct[i].cost}\n ");//This list off the title of the books inside the list

            int productSelect;

            if(Int32.TryParse(Console.ReadLine(), out productSelect) && ((productSelect) >= 0 && (productSelect) < codeProduct.Count)) 
                return codeProduct[productSelect];  //This is a limit to where the input must fall on a number in between 0 and the amount of inventory
            else
            {
                Console.WriteLine("We couldn't find that in our inventory, Press R Try again");
                
                goto BadInput;
            }
            
    }

    //This refers to all of the purchasing of a product and storing the order inside of the database
    public void AddBookToCart(Customer currentCustomer, StoreFront shopIn, Product shopProduct, Order currentOrder, int count)
    {
        if(count == 0 )
        {
            currentOrder.CustomerID = currentCustomer.Id;
            currentOrder.StoreID = shopIn.Id;
        }
        currentOrder.AddCartItems(shopProduct);
    }
    public void Checkout(Order currentOrder)
{
    Console.WriteLine("Order has Been Placed!");
    currentOrder.DatePurchased = DateTime.Now;
    if(_bl.AddCustomerHistory(currentOrder) == null)
        Console.WriteLine("Your Order has Been Placed");

}


}


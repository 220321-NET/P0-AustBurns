using Microsoft.Data.SqlClient;
using System.Data;
using Models;

namespace DL;

public class DBRepository : IRepository
{
    private readonly string _connectionString;

    public DBRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public int CheckLogin(Customer login)
    {
        bool found = false;

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE username = @username", connection);

        cmd.Parameters.AddWithValue("@username", login.Username);

        SqlDataReader read = cmd.ExecuteReader();
        if(read.HasRows)
            found = true;
        read.Close();

        if(found)
            return 1;
        
        return 0;

    }
    public Order AddCustomerHistory(Order updateHistory)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO Orders(DatePurchased, Total, StoreID, CustomerID)OUTPUT INSERTED.OrderID VALUES (@DatePurchased, @orderTotal, @StoreID, @CustomerID)", connection);// link all orderID's associated with a CustomerID 

        cmd.Parameters.AddWithValue("@DatePurchased", updateHistory.DatePurchased);
        cmd.Parameters.AddWithValue("@orderTotal", updateHistory.Total());
        cmd.Parameters.AddWithValue("@StoreID", updateHistory.StoreID);
        cmd.Parameters.AddWithValue("@CustomerID", updateHistory.CustomerID);

        try
        {
            updateHistory.Id = (int) cmd.ExecuteScalar();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        connection.Close();

        return updateHistory;
    } 
    public Order GetCustomerHistory(Order getHistory)
    {
        List<Order> history = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE CustomerID = @CustomerID", connection);
        cmd.Parameters.AddWithValue("@CustomerID", getHistory.Id);

        SqlDataReader read = cmd.ExecuteReader();

        // while(read.Read())
        // {
        //     Product codeProduct = SelectProduct(read.GetInt32(2));
        //     history.Add(codeProduct);
        // }

        read.Close();
        connection.Close();

        List<Order> SortedList = history.OrderBy(o=>o.Id).ToList();

        return getHistory;

    }
    public Customer SelectCustomer(Customer customer)
    {
        Customer returnCustomer = new Customer();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE username = @username", connection);
        cmd.Parameters.AddWithValue("@username", customer.Username);

        SqlDataReader read = cmd.ExecuteReader();

        if(read.Read())
        {
            int codeID = read.GetInt32(0);
            string username = read.GetString(1);

            returnCustomer.Id = codeID;
            returnCustomer.Username = username;
        }

        read.Close();
        connection.Close();

        return returnCustomer;







    }
    public Customer CreateCustomer(Customer customerToCreate)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("INSERT INTO Customers(username) OUTPUT INSERTED.id VALUES (@username)", connection);

        cmd.Parameters.AddWithValue("@username", customerToCreate.Username);


        try{

        cmd.ExecuteScalar();                        //This function takes in the user information and stores it inside of the table
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();

        return customerToCreate;
    }


    //This selects the inventory from a the store that the user inputs
    public List<Product> GetInventory(StoreFront getInv)
    {
        List<Product> inventory = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory WHERE StoreID = @StoreID", connection);
        cmd.Parameters.AddWithValue("@StoreID", getInv.Id);

        SqlDataReader read = cmd.ExecuteReader();

        while(read.Read())
        {
            Product codeProduct = SelectProduct(read.GetInt32(2));
            inventory.Add(codeProduct);
        }

        read.Close();
        connection.Close();

        List<Product> SortedList = inventory.OrderBy(o=>o.Id).ToList();

        return SortedList;
    }

    public List<LineItems> GetAllLineItems()
    {
        List<LineItems> allInventoryItems = new List<LineItems>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("SELECT * FROM LineItems", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            int LineItemID = reader.GetInt32(0);
            int StoreId = reader.GetInt32(1);
            int ProductId = reader.GetInt32(2);
            int Quantity = reader.GetInt32(3);
            LineItems inventoryItem = new LineItems{
                Id = LineItemID,
                storeId = StoreId,
                productId = ProductId,
                quantity = Quantity
            };
            allInventoryItems.Add(inventoryItem);
        }
        reader.Close();
        connection.Close();
        return allInventoryItems;
    }

    //This Lists out all the stores in the Database and allows the user to select which store they would like to shop at
    public List<StoreFront> SelectStore()
    {
        List<StoreFront> allStores = new List<StoreFront>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Stores", connection);
        SqlDataReader read = cmd.ExecuteReader();

        while(read.Read())
        {
            int StoreID = read.GetInt32(0);  //This takes the data inside the chart and reads it to be put into
            string city = read.GetString(1); //the allStores "temporary' store front named codeStoreFront
            string state = read.GetString(2);//

            StoreFront codeStoreFront = new StoreFront
            {
                Id = StoreID,                  //These take the read information from inside the chart and inserts it into the 
                StoreCity = city,              //StoreFront file by using ToString()
                StoreState = state
            };
            allStores.Add(codeStoreFront);
        }

        read.Close();
        connection.Close();

        return allStores;
    }
    public Product SelectProduct(int ProductID)
    {
        Product codeProduct = new Product();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE id = @id", connection);
        cmd.Parameters.AddWithValue("@id", ProductID);
        
        SqlDataReader read = cmd.ExecuteReader();

        if(read.Read())
        {
            int codeID = read.GetInt32(0); //(x) selects the column and stores it in a temporary storage to use in the UI
            string Title = read.GetString(1);
            string Content = read.GetString(2);
            double Cost =  read.GetDouble(3);

            codeProduct.Id = codeID;
            codeProduct.title = Title;
            codeProduct.content = Content;
            codeProduct.cost = Cost;
        }

        return codeProduct;
    }


    //This is all the Admin functionality
    public  Product CreateNewProduct(Product newProduct)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("INSERT INTO Products(Title, Contnt, Cost) OUTPUT INSERTED.CustomerID VALUES (@Title, @Content, @Cost)", connection);

        cmd.Parameters.AddWithValue("@Title", newProduct.title);
        cmd.Parameters.AddWithValue("@Content", newProduct.content);
        cmd.Parameters.AddWithValue("@Cost", newProduct.cost);

        try
        {
        cmd.ExecuteScalar();                        //This function takes in the user information and stores it inside of the table
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();

        return newProduct;

    }


    // public Product UpdateStock(Product updateStocks)
    // {
    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     connection.Open();

    //     using SqlCommand cmd = new SqlCommand("UPDATE TABLE Inventory(Quantity, ) OUTPUT INSERTED.id VALUES (@username)", connection);

    //     cmd.Parameters.AddWithValue("@username", updateStocks.Username);
    // }

}
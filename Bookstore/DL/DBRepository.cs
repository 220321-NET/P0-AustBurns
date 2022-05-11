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
    
    public async Task<int> CheckLoginAsync(string UserName)
    {
        bool found = false;

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE username = @username", connection);

        cmd.Parameters.AddWithValue("@username", UserName);

        SqlDataReader read = await cmd.ExecuteReaderAsync();
        if(read.HasRows)
            found = true;
        read.Close();

        if(found)
            return 1;
        
        return 0;

    }
    public async Task<Order> AddCustomerHistoryAsync(Order updateHistory)
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
    public async Task<List<Order>> GetCustomerHistory(Order getHistory)
    {
        List<Order> history = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE CustomerID = @CustomerID", connection);
        cmd.Parameters.AddWithValue("@CustomerID", getHistory.Id);

        SqlDataReader read = await cmd.ExecuteReaderAsync();

        // while(read.Read())
        // {
        //     Product codeProduct = SelectProduct(read.GetInt32(2));
        //     history.Add(codeProduct);
        // }

        read.Close();
        connection.Close();

        List<Order> SortedList = history.OrderBy(o=>o.Id).ToList();

        return SortedList;

    }
    public async Task<Customer> SelectCustomerAsync(string UserName)
    {
        Customer returnCustomer = new Customer();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE username = @username", connection);
        cmd.Parameters.AddWithValue("@username", UserName);

        SqlDataReader read = cmd.ExecuteReader();

        if(await read.ReadAsync())
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
    public async Task<Customer> CreateCustomerAsync(Customer customerToCreate)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd =  new SqlCommand("INSERT INTO Customers(username) OUTPUT INSERTED.id VALUES (@username)", connection);

        cmd.Parameters.AddWithValue("@username", customerToCreate.Username);

        await cmd.ExecuteScalarAsync();
        connection.Close();
        return customerToCreate;
    }


    //This selects the inventory from a the store that the user inputs
    public async Task<List<Inventory>> GetInventory(int StoreID)
    {
        List<Inventory> inventory = new List<Inventory>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory WHERE StoreID = @StoreID", connection);
        cmd.Parameters.AddWithValue("@StoreID", StoreID);

        SqlDataReader read = cmd.ExecuteReader();

        while(await read.ReadAsync())
        {
            Product codeProduct = await SelectProductAsync(read.GetInt32(2));
            int quantity = read.GetInt32(1);
            //inventory.Add(codeProduct);

            Inventory codeInv = new Inventory
            {
                invProduct = codeProduct,
                quantity = quantity
            };

            inventory.Add(codeInv);
        }

        read.Close();
        connection.Close();

        List<Inventory> SortedList = inventory.OrderBy(o=>o.invProduct.Id).ToList();

        return SortedList;
    }

    //This Lists out all the stores in the Database and allows the user to select which store they would like to shop at
    public async Task<List<StoreFront>> SelectStoreAsync()
    {
        List<StoreFront> allStores = new List<StoreFront>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Stores", connection);
        SqlDataReader read = cmd.ExecuteReader();

        while(await read.ReadAsync())
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
    public async Task<Product> SelectProductAsync(int id)
    {
        Product codeProduct = new Product();

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE id = @ProductID", connection);
        cmd.Parameters.AddWithValue("@ProductID", id);
        
        SqlDataReader read = cmd.ExecuteReader();

        if(await read.ReadAsync())
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

        cmd.ExecuteScalar();
        connection.Close();

        return newProduct;

    }
    public async Task<List<Product>> AllProductsAsync()
        {
            List<Product> allProducts = new List<Product>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand cmd = new SqlCommand("SELECT * FROM Products", connection);
            SqlDataReader read = await cmd.ExecuteReaderAsync();

            while(read.Read())
            {
                int id = read.GetInt32(0);  //This takes the data inside the chart and reads it to be put into
                string Title = read.GetString(1); //the allStores "temporary' store front named codeStoreFront
                string Content = read.GetString(2);//
                double Cost = read.GetDouble(3);

                Product codeAdminAllInv = new Product
                {
                    Id = id,                  //These take the read information from inside the chart and inserts it into the 
                    title = Title,              //StoreFront file by using ToString()
                    content = Content,
                    cost = Cost
                };
                allProducts.Add(codeAdminAllInv);
            }

            read.Close();
            connection.Close();

            return allProducts;
        }

}
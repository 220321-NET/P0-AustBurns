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




/// <summary>
/// Returns a list of all User purchases in the Products table 
/// </summary>
/// <returns>List of purchases</returns>
    public Customer CustomerHistory(Customer getHistory)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM Customers");
        SqlDataReader reader = cmd.ExecuteReader();

        //ask about what other values go here
        return getHistory;
    }

    /// <summary>
    /// Creates a new Customer
    /// </summary>
    /// <returns>new customer</returns>
    
    public Customer CreateCustomer(Customer customerToCreate)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("INSERT INTO Customers(username) OUTPUT INSERTED.CustomerID VALUES (@username)", connection);

        cmd.Parameters.AddWithValue("@username", customerToCreate.Username);


        try{

        cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();

        return customerToCreate;
    }


    // public Product GetAllBooks()
    // {
    //     SqlConnection connection = new SqlConnection(_connectionString);
    //     connection.Open();

    //     SqlCommand cmd = new SqlCommand("SELECT * FROM Products");
    //     SqlDataReader reader = cmd.ExecuteReader();
    //     if(reader.HasRows)
    //     {

    //     }
    // }
    
}
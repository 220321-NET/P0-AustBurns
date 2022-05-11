using System.Net.Http;
using System.Text.Json;
using System.Text;
using Models;


namespace UI;

public class HttpService
{
    private readonly string _apiBaseUrl = " http://localhost:7141/api/";    //Find out which number needs to be inside of the question marks

    private HttpClient client = new HttpClient();

    public HttpService()
    {
        client.BaseAddress = new Uri(_apiBaseUrl);
    }

    public async Task<int> CheckLoginAsync(string username)
    {
        int output = 0;
        try
        {
            HttpResponseMessage response = await client.GetAsync($"Customer/GetLogin/{username}");
            response.EnsureSuccessStatusCode();
            string responsestring = await response.Content.ReadAsStringAsync();
            output = JsonSerializer.Deserialize<int>(responsestring);
        }
        catch(HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return output;
    }

    public async Task<Customer> SelectCustomerAsync(string UserName)
    {
        Customer customer = new Customer();
        try
        {
            HttpResponseMessage response = await client.GetAsync($"Customer/GetLogin/{UserName}");
            response.EnsureSuccessStatusCode();
            string responsestring = await response.Content.ReadAsStringAsync();
            customer = JsonSerializer.Deserialize<Customer>(responsestring);
        }
        catch(HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return customer;
    }
    public async Task<Customer> CreateCustomerAsync(Customer customerToCreate)
    {
        string json = JsonSerializer.Serialize(customerToCreate);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync("Customer/CreateCustomer", content);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<Customer>(await response.Content.ReadAsStreamAsync()) ?? new Customer();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }

    public async Task<Order> AddCustomerHistoryAsync(Order updateHistory)
    {
        string json = JsonSerializer.Serialize(updateHistory);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync("Order/CreateOrder", content);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<Order>(await response.Content.ReadAsStreamAsync()) ?? new Order();
        }
        catch (HttpRequestException)
        {
            throw;
        }
    }






    public async Task<List<StoreFront>> SelectStoreAsync()
    {
        List<StoreFront> stores = new List<StoreFront>();
        try
        {   
            HttpResponseMessage response = await client.GetAsync($"Store/SelectStore");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            stores = JsonSerializer.Deserialize<List<StoreFront>>(responseString) ?? new List<StoreFront>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Couldn't Find Stores");
        }
        return stores;
    }
}
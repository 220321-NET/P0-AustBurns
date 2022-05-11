using UI;


string connectionString = File.ReadAllText("./connectionString.txt");

HttpService http = new HttpService();

await new MainMenu(http).Start();
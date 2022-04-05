using UI;
using BL;
using DL;


string connectionString = File.ReadAllText("./connectionString.txt");

IRepository repo = new DBRepository(connectionString);
ISLBL b1 = new SLBL(repo);
new MainMenu(b1).Start();
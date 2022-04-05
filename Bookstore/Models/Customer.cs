using System.ComponentModel.DataAnnotations;
namespace Models;
    public class Customer 
    {
        //public string username = "";
private string username;
        public string Username
        {
            get => Username;
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                    {
                        throw new ValidationException("Must Provide a Username");
                    }

                username = value;//.Trim();
            }
        }

    }

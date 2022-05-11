using System.ComponentModel.DataAnnotations;

namespace Models;
    public class Customer : BasicID
    {
        private string? username = "";
                public string? Username
                {
                    get => username;
                    set
                    {
                        if(String.IsNullOrWhiteSpace(value))
                            {
                                throw new ValidationException("Must Provide a Username");
                            }

                        username = value;
                    }
                }

            }

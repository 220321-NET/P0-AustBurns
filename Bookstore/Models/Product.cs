using System.ComponentModel.DataAnnotations;
namespace Models;

public class Product : BasicID
{
    private string Title = "";
    private string Content = "";
    private double Cost = 0.00f;

    public string title
    {
        get => Title;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ValidationException("The Book MUST have a Title to Exist!");

            Title = value;
                
        }
    }

        public string content
    {
        get => Content;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ValidationException("The Book MUST have content to Exist!");

            Content = value;
                
        }
    }

        public double cost
    {
        get => Cost;
        set
        {
            if (Double.IsNegative(value))
                throw new ValidationException("We cant pay you for the book we own!");

            Cost = value;
                
        }
    }
    public override string ToString()
    {
        return $"[{Id}]: {Title}, {Content}, {Cost} ";
    }
}
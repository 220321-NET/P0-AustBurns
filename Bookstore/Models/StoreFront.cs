using System.ComponentModel.DataAnnotations;

public class StoreFront : BasicID
{
    private string city = "";

    private string state = "";
    
    public string StoreCity 
    {
        get => city;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ValidationException("The Store MUST have a location to Exist!");

            city = value;
        }
    }

    public string StoreState
    {
        get => state;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ValidationException("The Store MUST have a location to Exist!");

        state = value;
        }
    }

    public override string ToString()
    {
        return $"[{Id}]: {city}, {state} ";
    }
}
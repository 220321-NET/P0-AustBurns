using System.ComponentModel.DataAnnotations;

namespace Models;  //This is used to give all of the tables that you pull a basic id that you can use in the UI

public abstract class BasicID
{
    public int Id {get; set;}

}
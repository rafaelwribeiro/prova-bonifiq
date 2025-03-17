namespace ProvaPub.Models;

public class ModelList<T>
{
    public IList<T> List { get; set; }
    public int TotalCount { get; set; }
    public bool HasNext { get; set; }
}

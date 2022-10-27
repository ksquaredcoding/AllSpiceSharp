namespace AllSpiceSharp.Models;

public class Favorite : IRepoItem<string>
{
  public string Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string AccountId { get; set; }
  public int recipeId { get; set; }
}
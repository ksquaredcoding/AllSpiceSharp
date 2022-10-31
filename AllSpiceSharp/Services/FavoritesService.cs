namespace AllSpiceSharp.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _fr;

  public FavoritesService(FavoritesRepository fr)
  {
    _fr = fr;
  }

  internal Favorite CreateFavorite(Favorite newFavorite)
  {
    return _fr.CreateFavorite(newFavorite);
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _fr.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception("Favorite does not exist");
    }
    return favorite;
  }

  internal List<FavRecipe> GetFavoritesByAccount(string accountId)
  {
    return _fr.GetFavoritesByAccount(accountId);
  }

  internal void RemoveFavorite(int favoriteId, string accountId)
  {
    Favorite fav = GetFavoriteById(favoriteId);
    if (fav.AccountId != accountId)
    {
      throw new Exception("Unfavorite your own stuff.");
    }
    _fr.RemoveFavorite(fav);
  }
}
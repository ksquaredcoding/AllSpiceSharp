namespace AllSpiceSharp.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _fr;

  public FavoritesService(FavoritesRepository fr)
  {
    _fr = fr;
  }
}
using NatureBreaks.Models;
using System.Collections.Generic;

namespace NatureBreaks.Interfaces
{
    public interface IFavoriteVideoRepository
    {
        void AddFavorite(FavoriteVideo favoriteVideo);
        void DeleteFavoriteById(int id);
        List<FavoriteVideo> GetAllFavorites();
        FavoriteVideo GetFavoriteById(int id);
    }
}
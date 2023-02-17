using NatureBreaks.Models;
using System.Collections.Generic;

namespace NatureBreaks.Interfaces
{
    public interface IFavoriteVideosRepository
    {
        void AddFavorite(FavoriteVideos favoriteVideo);
        void DeleteFavoriteById(int id);
        List<FavoriteVideos> GetAllFavorites(int id);
        List<FavoriteVideos> GetFavoriteById(int id);
    }
}
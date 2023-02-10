using NatureBreaks.Models;
using System.Collections.Generic;

namespace NatureBreaks.Interfaces
{
    public interface IVideoRepository
    {
        void Add(Video vid);
        void Delete(int id);
        Video Get(int id);
        List<Video> GetAllVideos();
        void Update(Video vid);
    }
}
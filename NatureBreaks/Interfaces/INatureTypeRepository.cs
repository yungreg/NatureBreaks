using NatureBreaks.Models;
using System.Collections.Generic;

namespace NatureBreaks.Interfaces
{
    public interface INatureTypeRepository
    {
        void AddNatureType(NatureType natureType);
        void DeleteNatureTypeById(int id);
        List<NatureType> GetAllNatureTypes();
        NatureType GetNatureTypeById(int id);
    }
}
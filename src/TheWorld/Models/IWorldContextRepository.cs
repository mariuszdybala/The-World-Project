using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldContextRepository<T>
    {
        IQueryable<T> GetAllDate();
        void SaveData(T itemToSave);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextRepository : IWorldContextRepository<Trip>
    {
        public WorldConext _context { get;  set; }

        public WorldContextRepository(WorldConext context)
        {
            _context = context;
        }


        public IQueryable<Trip> GetAllDate()
        {
            return _context.Trips;
        }

        public void SaveData(Trip itemToSave)
        {
            _context.Trips.Add(itemToSave);
        }
    }
}

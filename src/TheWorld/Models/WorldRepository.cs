using Microsoft.Data.Entity;
using Microsoft.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.ViewModels;

namespace TheWorld.Models
{
    public class WorldRepository: IWorldRepository

    {
        public WorldConext _context { get; private set; }
        public ILogger<WorldRepository> _logger { get; private set; }

        public WorldRepository(WorldConext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(x => x.Name).ToList();
            }
            catch (Exception)
            {
                _logger.LogError("Could not get Trips");
                return null;
            }
        }
        public IEnumerable<Trip>GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips
                        .Include(x => x.Stops)
                        .OrderBy(x => x.Name)
                        .ToList();
            }
            catch (Exception)
            {
                _logger.LogError("Could not get Trips with Stops");
                throw;
            }
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Trips.Add(newTrip);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() >= 1 ;
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips.Include(x => x.Stops).Where(x => x.Name == tripName).FirstOrDefault();
        }

        public void SaveStop(string tripName, Stop stop)
        {
            var theTrip = GetTripByName(tripName);
            stop.Order = theTrip.Stops.Max(x => x.Order) + 1;
            theTrip.Stops.Add(stop);
            _context.Stops.Add(stop);
        }
    }
}

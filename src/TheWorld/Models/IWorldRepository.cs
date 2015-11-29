using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.ViewModels;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip newTrip);
        bool SaveAll();
        Trip GetTripByName(string tripName, string userName);
        void SaveStop(string tripName, string userName,Stop stop);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}

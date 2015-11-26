using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace TheWorld.Models
{
    public class WorldContextDBFeed
    {
        public WorldConext _context { get; set; }

        public WorldContextDBFeed(WorldConext context)
        {
            _context = context;
        }

        public void EnsureSeedData()
        {
            if(!_context.Trips.Any())
            {
                var usTrip = new Trip()
                {
                    Name = "Us Trip",
                    Created = DateTime.UtcNow,
                    UserName = "",
                    Stops = new List<Stop>()
                    {
                        new Stop() {Name="New York", Order = 1, Arrival = new DateTime(2015,01,01), Latitude=50, Longitude=20 },
                        new Stop() {Name="Atlanta", Order = 2, Arrival = new DateTime(2015,01,15),  Latitude=52, Longitude=22 },
                        new Stop() {Name="Los Angeles", Order = 3, Arrival = new DateTime(2015,01,20), Latitude=54, Longitude=24 }
                    }
                };

                _context.Trips.Add(usTrip);
                _context.Stops.AddRange(usTrip.Stops);

                var euTrip = new Trip()
                {
                    Name = "Europe Trip",
                    Created = DateTime.UtcNow,
                    UserName = "",
                    Stops = new List<Stop>()
                    {
                        new Stop() {Name="London", Order = 1, Arrival = new DateTime(2015,02,01), Latitude=40, Longitude=10 },
                        new Stop() {Name="Paris", Order = 2, Arrival = new DateTime(2015,02,15),  Latitude=42, Longitude=12 },
                        new Stop() {Name="Krakow", Order = 3, Arrival = new DateTime(2015,02,20), Latitude=44, Longitude=14 }
                    }
                };
                _context.Trips.Add(euTrip);
                _context.Stops.AddRange(euTrip.Stops);

                _context.SaveChanges();
            }
        }
    }
}

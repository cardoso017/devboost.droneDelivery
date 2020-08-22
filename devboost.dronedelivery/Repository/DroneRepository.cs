using devboost.dronedelivery.Model;
using devboost.dronedelivery.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Repository
{
    public class DroneRepository
    {
        readonly DataContext _dataContext;

        public DroneRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Drone>> GetDisponiveis()
        {
            return await _dataContext.Drone.Where(x => (int)x.StatusDrone == (int)StatusDrone.disponivel).ToListAsync();
        }

        public async Task UpdateDrone(Drone drone)
        {
            _dataContext.Drone.Update(drone);
            await _dataContext.SaveChangesAsync();
        }
    }
}

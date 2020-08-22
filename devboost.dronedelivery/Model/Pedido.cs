using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Model
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public int Peso { get; set; }
        public DbGeography LatLong { get; set; }
        public DateTime DataHora { get; set; }
        public int DroneId { get; set; }
        public Drone Drone { get; set; }

        public StatusPedido StatusPedido { get; set; }
    }

    public enum StatusPedido
    {
        aguardandoAprovacao = 0,
        reprovado = 1,
        aguardandoEntrega = 2,
        despachado = 3,
        entregue = 4
    }
}

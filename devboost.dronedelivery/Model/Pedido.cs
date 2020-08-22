using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Model
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public int Peso { get; set; }
        //todo: trocar o tipo para geograph
        public double LatLong { get; set; }
        public DateTime DataHora { get; set; }
        public int DroneId { get; set; }
        public Drone Drone { get; set; }

        public StatusPedido statusPedido { get; set; }
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

using devboost.dronedelivery.Model;
using devboost.dronedelivery.Repository;
using GeoLocation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace devboost.dronedelivery.Service
{
    public class PedidoService
    {
        readonly static DbGeography LOCATION_ORIGINB = DbGeography.FromText("POINT(-23.5880684 -46.6564195)");

        readonly PedidoRepository _pedidoRepository;
        readonly DroneRepository _droneRepository;

        public PedidoService(PedidoRepository pedidoRepository, DroneRepository droneRepository)
        {
            _pedidoRepository = pedidoRepository;
            _droneRepository = droneRepository;
        }

        public async void RealizarPedido(Pedido pedido) 
        {
            using (var trans = new TransactionScope())
            {
                //Verificar a distância entre Origem e Destindo (Pedido)
                var distance = new Coordinates(LOCATION_ORIGINB.Latitude.Value, LOCATION_ORIGINB.Longitude.Value)
               .DistanceTo(
                   new Coordinates(pedido.LatLong.Latitude.Value, pedido.LatLong.Longitude.Value),
                   UnitOfLength.Kilometers
               );
                var drones = await _droneRepository.GetDisponiveis();
                //Verificar drones, que possuem autonomia de ida e volta

                //Qual automomia atual do drone = (Autonomia * Carga) / 100
                //Temos que pegar os Drones com AA >= Distancia do Pedido * 2
                var dronesDispAutonomia = drones?.Where(x => ((x.Autonomia * x.Carga) / 100) >= (distance * 2))?.ToList();

                //Dos Drones com autonomia, quais podem carregar o peso do pedido
                var dronesComCapacidade = dronesDispAutonomia?.Where(x => x.Capacidade >= pedido.Peso)?.ToList();

                //Caso dronesComCapacidade não seja nulo e contenha objetos (drone), pode ser responsável pela entrega
                if (dronesComCapacidade != null && dronesComCapacidade.Count() > 0)
                {
                    var drone = dronesComCapacidade.FirstOrDefault();
                    pedido.Drone = drone;
                    pedido.StatusPedido = StatusPedido.despachado;
                    drone.StatusDrone = StatusDrone.emTrajeto;
                    _pedidoRepository.AddPedido(pedido);
                    _droneRepository.UpdateDrone(drone);
                }
                else
                {
                    pedido.StatusPedido = StatusPedido.reprovado;
                    _pedidoRepository.UpdatePedido(pedido);
                }
                trans.Complete();
            }
        }
    }
}

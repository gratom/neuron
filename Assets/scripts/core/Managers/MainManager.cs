using System;
using Global;
using Unity.Mathematics;
using UnityEngine;

namespace Managers.Datas
{
    public class MainManager : BaseManager
    {

        [SerializeField] private SpaceShipControl playerPrefab;

        public override Type ManagerType => typeof(MainManager);

        public void EntryPoint()
        {
            StartAsteroidsManager();
            StartOtherSpaceShipsManager();
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            SpaceShipControl player = Instantiate(playerPrefab);
            Services.GetManager<OtherSpaceshipManager>().SetPlayer(player);
        }

        private void StartOtherSpaceShipsManager()
        {
            Services.GetManager<OtherSpaceshipManager>().StartWork();
        }

        private void StartAsteroidsManager()
        {

            Services.GetManager<AsteroidManager>().StartWork();
        }
    }
}

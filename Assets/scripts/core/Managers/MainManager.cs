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

        }
    }
}

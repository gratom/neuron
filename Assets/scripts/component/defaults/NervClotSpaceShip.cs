using System.Collections.Generic;
using Global.Component.Perceptron;
using UnityEngine;

namespace Global.Component.Defaults
{
    public class NervClotSpaceShip : NervClot
    {
        private List<ILink> links = new List<ILink>();

        public override List<ILink> Links => links;

        [SerializeField] private SpaceShipControl spaceShip;

        private void Awake()
        {
            links.Add(new Nerv(() => spaceShip.Money));
            links.Add(new Nerv(() => spaceShip.HP));
        }
    }
}
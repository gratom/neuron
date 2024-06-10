using System.Collections.Generic;
using Global.Component.Perceptron;
using Managers.Datas;
using UnityEngine;

namespace Global.Component.Defaults
{
    public class NervClotNearestSpaceShip : NervClot
    {
        private List<ILink> links = new List<ILink>();
        [SerializeField] private SpaceShipControl ship;

        public override List<ILink> Links => links;

        private OtherSpaceshipManager spaceshipManager => Services.GetManager<OtherSpaceshipManager>();
        private Vector2 DiffNearestSpaceShip => spaceshipManager.GetNearestShipPos(new Vector2(transform.position.x, transform.position.z));
        private Vector2 DirToShip => DiffNearestSpaceShip - new Vector2(transform.position.x, transform.position.z);

        private void Awake()
        {
            links.Add(new Nerv(() => DirToShip.x));
            links.Add(new Nerv(() => DirToShip.y));
            links.Add(new Nerv(() => DirToShip.magnitude));
            links.Add(new Nerv(() =>
            {
                float angle = Vector2.SignedAngle(new Vector2(transform.forward.x, transform.forward.z), DirToShip.normalized);
                return angle;
            }));
        }
    }

}

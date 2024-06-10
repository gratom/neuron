using System.Collections.Generic;
using Global.Component.Perceptron;
using Managers.Datas;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Component.Defaults
{
    public class NervClotNearestAsteroid : NervClot
    {
        private List<ILink> links = new List<ILink>();

        [SerializeField] private SpaceShipControl ship;
        public override List<ILink> Links => links;

        private AsteroidManager asteroidManager => Services.GetManager<AsteroidManager>();

        private Vector2 DiffNearestAsteroid => asteroidManager.GetNearestAsteroid(new Vector2(transform.position.x, transform.position.z));
        private Vector2 DirToAsteroid => DiffNearestAsteroid - new Vector2(transform.position.x, transform.position.z);

        private void Awake()
        {
            links.Add(new Nerv(() => DirToAsteroid.x));
            links.Add(new Nerv(() => DirToAsteroid.y));
            links.Add(new Nerv(() => DirToAsteroid.magnitude));
            links.Add(new Nerv(() =>
            {
                float angle = Vector2.SignedAngle(new Vector2(transform.forward.x, transform.forward.z), DirToAsteroid.normalized);
                return angle;
            }));
        }
    }
}

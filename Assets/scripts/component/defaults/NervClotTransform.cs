using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Defaults
{
    using Global.Component.Perceptron;

    public class NervClotTransform : NervClot
    {
#pragma warning disable

        [SerializeField] private bool trackPosition;
        [SerializeField] private bool trackRotation;

#pragma warning restore

        private List<ILink> links = new List<ILink>();

        public override List<ILink> Links { get => links; }

        private void Awake()
        {
            if (trackPosition)
            {
                links.Add(new Nerv(() => { return transform.position.x; }));
                links.Add(new Nerv(() => { return transform.position.y; }));
                links.Add(new Nerv(() => { return transform.position.z; }));
            }
            if (trackRotation)
            {
                links.Add(new Nerv(() => { return transform.rotation.eulerAngles.x; }));
                links.Add(new Nerv(() => { return transform.rotation.eulerAngles.y; }));
                links.Add(new Nerv(() => { return transform.rotation.eulerAngles.z; }));
            }
        }
    }
}
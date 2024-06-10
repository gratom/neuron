using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Defaults
{
    using Global.Component.Perceptron;

    public class NervClotRigidbody : NervClot
    {
#pragma warning disable

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private bool trackVelocity;
        [SerializeField] private bool trackAngularVelocity;

#pragma warning restore

        private List<ILink> links = new List<ILink>();

        public override List<ILink> Links { get => links; }

        private void Awake()
        {
            if (trackVelocity)
            {
                links.Add(new Nerv(() => { return rigidbody.velocity.x; }));
                links.Add(new Nerv(() => { return rigidbody.velocity.y; }));
                links.Add(new Nerv(() => { return rigidbody.velocity.z; }));
            }
            if (trackAngularVelocity)
            {
                links.Add(new Nerv(() => { return rigidbody.angularVelocity.x; }));
                links.Add(new Nerv(() => { return rigidbody.angularVelocity.y; }));
                links.Add(new Nerv(() => { return rigidbody.angularVelocity.z; }));
            }
        }
    }
}
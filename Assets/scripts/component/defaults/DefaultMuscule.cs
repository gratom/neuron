using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Defaults
{
    using Global.Component.Perceptron;

    public class DefaultMuscule : Muscule
    {
#pragma warning disable
        [SerializeField] private new HingeJoint joint;
        [SerializeField] private float multiplicator;
#pragma warning restore

        private JointSpring spring = new JointSpring();

        private void Awake()
        {
            if (joint != null)
            {
                spring = joint.spring;
            }
        }

        public override void Make(float value)
        {
            if (joint != null)
            {
                spring.targetPosition = value * multiplicator;
                joint.spring = spring;
            }
        }
    }
}
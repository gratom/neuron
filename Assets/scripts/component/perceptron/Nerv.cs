using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Perceptron
{
    public class Nerv : ILink
    {
        public delegate float NervValueDelegate();

        private NervValueDelegate nervValueDelegate;

        public float Koef { get; set; }
        public float PureValue => nervValueDelegate?.Invoke() ?? 0;

        public float ResultValue => PureValue * Koef;

        public Nerv(NervValueDelegate nervValueDelegate)
        {
            this.nervValueDelegate = nervValueDelegate;
        }
    }
}

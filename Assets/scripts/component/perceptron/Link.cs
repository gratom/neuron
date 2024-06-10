using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Perceptron
{
    public interface ILink
    {
        float Koef { get; set; }
        float PureValue { get; }
        float ResultValue { get; }
    }

    public class Link : ILink
    {
        private Neuron neuron;

        public float Koef { get; set; }

        public float ResultValue => neuron.Value * Koef;
        public float PureValue => neuron.Value;

        public Link(Neuron neuron)
        {
            this.neuron = neuron;
        }

    }
}

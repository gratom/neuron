using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Perceptron
{
    [Serializable]
    public class Layer
    {
        public List<Neuron> neurons;
    }

    [Serializable]
    public class SavedLayer
    {
        public List<SavedNeuron> neurons;
    }

    [Serializable]
    public class SavedNeuron
    {
        public float[] koefs;
    }

}

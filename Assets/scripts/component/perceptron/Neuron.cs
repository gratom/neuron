using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Global.Component.Perceptron
{
    [Serializable]
    public class Neuron
    {
        public enum ActivationFunction
        {
            sigmoidZeroOne,
            sigmoidMinusOneOne
        }

        private delegate float ActivationFunctionDelegate(float sum, float multiplier);

#pragma warning disable
        [SerializeField] private float value;
        [SerializeField] private float multiplier = 5f;
        [SerializeField] private ActivationFunction functionsType;
#pragma warning restore

        private Dictionary<ActivationFunction, ActivationFunctionDelegate> functionsDictionary = new Dictionary<ActivationFunction, ActivationFunctionDelegate>()
        {
            {
                ActivationFunction.sigmoidZeroOne, (sum, multiplier) => Mathf.Pow(1 + Mathf.Exp(-sum * multiplier), -1)
            },
            {
                ActivationFunction.sigmoidMinusOneOne, (sum, multiplier) => Mathf.Pow(1 + Mathf.Exp(-sum * multiplier), -1) * 2 - 1
            }
        };

        private ActivationFunctionDelegate currentFunction;

        public List<ILink> Links;

        public float[] state
        {
            get
            {
                float[] ret = new float[Links.Count];
                for (int i = 0; i < ret.Length; i++)
                {
                    ret[i] = Links[i].Koef;
                }
                return ret;
            }
            set
            {
                for (int i = 0; i < Links.Count; i++)
                {
                    Links[i].Koef = value[i];
                }
            }
        }

        public Neuron(int layer, int index)
        {
            Layer = layer;
            Index = index;
        }

        public float Multiplier { get => multiplier; set => multiplier = value; }

        public int Layer { get; private set; }

        public int Index { get; private set; }

        public float Value => value;

        public void RecalculateValue()
        {
            float sum = Links.Sum(x => x.ResultValue);
            value = Mathf.Pow(1 + Mathf.Exp(-sum * Multiplier), -1) * 2 - 1;
        }
    }
}

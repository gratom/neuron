using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

namespace Global.Component.Perceptron
{
    [Serializable]
    public class BrainState
    {
        public List<SavedLayer> layers;
    }

    public class Brain : MonoBehaviour
    {
#pragma warning disable

        [SerializeField] private string strData;

        [SerializeField] private List<Layer> layers;
        [SerializeField] private BrainSettings brainSettings;
        [SerializeField] private List<NervClot> clots;

#pragma warning restore

        public BrainSettings BrainSettings => brainSettings;

        public bool HaveSavedData => !string.IsNullOrEmpty(strData);
        public List<Layer> Layers => layers;

        #region public functions

        public void CreateNewBrain()
        {
            layers = new List<Layer>
            {
                new Layer()
            };
            layers[0].neurons = new List<Neuron>();
            int neuronCounter = 0;
            for (int i = 0; i < clots.Count; i++)
            {
                for (int j = 0; j < clots[i].Links.Count; j++, neuronCounter++)
                {
                    layers[0].neurons.Add(new Neuron(0, neuronCounter));
                    layers[0].neurons[neuronCounter].Links = new List<ILink>() { clots[i].Links[j] };
                }
            }

            for (int i = 1; i < BrainSettings.layers.Length; i++)
            {
                layers.Add(new Layer());
                layers[i].neurons = new List<Neuron>();
                for (int j = 0; j < BrainSettings.layers[i]; j++)
                {
                    layers[i].neurons.Add(new Neuron(i, j));
                    layers[i].neurons[j].Links = new List<ILink>();

                    for (int k = 0; k < layers[i - 1].neurons.Count; k++)
                    {
                        layers[i].neurons[j].Links.Add(new Link(layers[i - 1].neurons[k]));
                    }
                }
            }
        }

        [ContextMenu("To Str Data")]
        public void ToStrData()
        {
            BrainState bTemp = new BrainState() { layers = new List<SavedLayer>(Layers.Count) };
            foreach (Layer layer in Layers)
            {
                bTemp.layers.Add(new SavedLayer() { neurons = new List<SavedNeuron>(layer.neurons.Count) });
                foreach (Neuron neuron in layer.neurons)
                {
                    bTemp.layers.Last().neurons.Add(new SavedNeuron() { koefs = neuron.state });
                }
            }
            strData = JsonUtility.ToJson(bTemp);
        }

        [ContextMenu("From Str Data")]
        public void FromStrData()
        {
            BrainState bTemp = JsonUtility.FromJson<BrainState>(strData);
            CopyFrom(bTemp);
        }

        public void SetRandom()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    for (int k = 0; k < layers[i].neurons[j].Links.Count; k++)
                    {
                        layers[i].neurons[j].Links[k].Koef = Random.Range(-1f, 1f);
                    }
                }
            }
        }

        public void CopyFrom(BrainState otherBrain)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    layers[i].neurons[j].state = otherBrain.layers[i].neurons[j].koefs;
                }
            }
        }


        public void MutateFrom(Brain otherBrain, float value)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    for (int k = 0; k < layers[i].neurons[j].Links.Count; k++)
                    {
                        layers[i].neurons[j].Links[k].Koef = otherBrain.layers[i].neurons[j].Links[k].Koef + Random.Range(-value, value);
                    }
                }
            }
        }

        public void MutateFrom(Brain otherBrain1, Brain otherBrain2, float value)
        {
            for (int i = 0; i < layers.Count; i++)
            {
                for (int j = 0; j < layers[i].neurons.Count; j++)
                {
                    for (int k = 0; k < layers[i].neurons[j].Links.Count; k++)
                    {
                        float koef = (otherBrain1.layers[i].neurons[j].Links[k].Koef + otherBrain2.layers[i].neurons[j].Links[k].Koef) / 2f;
                        layers[i].neurons[j].Links[k].Koef = koef + Random.Range(-value, value);
                    }
                }
            }
        }

        public void LoadFrom()
        {
        }

        public void RecalculateValues()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                foreach (Neuron t in layers[i].neurons)
                {
                    t.RecalculateValue();
                }
            }
        }

        #endregion public functions
    }

    [Serializable]
    public class BrainSettings
    {
        public int[] layers;
    }
}

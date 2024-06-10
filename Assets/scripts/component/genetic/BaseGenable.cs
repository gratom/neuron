using Global.Component.Perceptron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Genetic
{
    public abstract class BaseGenable : MonoBehaviour
    {
        public abstract float Value { get; }

        public abstract Perceptron.Brain Brain { get; }

        public abstract void Refresh();
    }
}
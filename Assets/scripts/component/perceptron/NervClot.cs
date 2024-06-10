using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Perceptron
{
    public abstract class NervClot : MonoBehaviour
    {
        public abstract List<ILink> Links { get; }
    }
}
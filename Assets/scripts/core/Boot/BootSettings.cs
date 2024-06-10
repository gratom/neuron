using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boot.Main
{
    using Managers;

    [CreateAssetMenu(fileName = "BootSettings", menuName = "Boot setting", order = 51)]
    public class BootSettings : ScriptableObject
    {
#pragma warning disable
        [SerializeField] private float bootTime;
        public float BootTime => bootTime;

        [SerializeField] private int nextSceneIndex;
        public int NextSceneIndex => nextSceneIndex;

        [SerializeField] private List<BaseManager> managers;
        public List<BaseManager> Managers => managers;
#pragma warning restore
    }
}
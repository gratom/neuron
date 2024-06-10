using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Organisms.Sworm
{
    using Global.Component.Genetic;
    using Global.Component.Perceptron;

    public class Sworm : BaseGenable
    {
#pragma warning disable

        [SerializeField] private Brain brain;
        [SerializeField] private List<Muscule> muscules;
        [SerializeField] private float lifeCicleTime = 0.1f;

#pragma warning restore

        private List<Vector3> startPositions = new List<Vector3>();
        private List<Quaternion> startRotations = new List<Quaternion>();

        public override float Value => muscules[muscules.Count - 1].transform.position.x - dopValue;

        public override Brain Brain => brain;

        private float dopValue = 100;

        public void Start()
        {
            for (int i = 0; i < muscules.Count; i++)
            {
                startPositions.Add(muscules[i].transform.localPosition);
                startRotations.Add(muscules[i].transform.rotation);
            }
            Brain.CreateNewBrain();
            Brain.SetRandom();
            StartCoroutine(LifeCoroutine());
        }

        public override void Refresh()
        {
            for (int i = 0; i < muscules.Count; i++)
            {
                muscules[i].transform.localPosition = startPositions[i];
                muscules[i].transform.rotation = startRotations[i];
                muscules[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                muscules[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }

        private void LifeCicle()
        {
            float f = Vector2.Distance(muscules[muscules.Count - 1].transform.position, muscules[0].transform.position);
            if (dopValue > f)
            {
                dopValue = f;
            }
            brain.RecalculateValues();
            for (int i = 0; i < brain.Layers[brain.Layers.Count - 1].neurons.Count; i++)
            {
                muscules[i].Make(brain.Layers[brain.Layers.Count - 1].neurons[i].Value);
            }
        }

        private IEnumerator LifeCoroutine()
        {
            while (true)
            {
                LifeCicle();
                yield return new WaitForSeconds(lifeCicleTime);
            }
        }
    }
}
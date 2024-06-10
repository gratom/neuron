using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Component.Genetic
{
    public abstract class BaseGenetic : MonoBehaviour
    {
#pragma warning disable
        [SerializeField] private GeneticSettings settings;
#pragma warning restore

        public abstract BaseGenable Prefab { get; }
        protected GeneticSettings Settings => settings;

        protected List<BaseGenable> GenableList = new List<BaseGenable>();

        protected bool isSimulated = true;

        protected Coroutine geneticCoroutine;

        #region Unity functions

        private void Start()
        {
            for (int i = 0; i < Settings.PopulationCount; i++)
            {
                BaseGenable genable = Instantiate(Prefab, transform);
                GenableList.Add(genable);
                ActionOnSpawn(ref genable, i);
            }

            geneticCoroutine = StartCoroutine(GeneticCoroutine());
        }

        #endregion Unity functions

        #region public functions

        public void SetSimulator(bool b)
        {
            isSimulated = b;
        }

        public void SwitchSimulation()
        {
            isSimulated = !isSimulated;
        }

        #endregion public functions

        #region abstract functions

        protected abstract void ActionOnSpawn(ref BaseGenable genable, int index);

        protected abstract void ActionOnNewGeneration(ref BaseGenable genable, int index);

        #endregion abstract functions

        #region private functions

        private IEnumerator GeneticCoroutine()
        {
            float timeStart = Time.time;
            while (true)
            {
                yield return new WaitForSeconds(Settings.PopulationTime);
                if (isSimulated)
                {
                    Time.timeScale = 0;
                    GenableList.Sort((x, y) => (int)(y.Value - x.Value));

                    Debug.Log("Best:" + GenableList[0].Value);

                    for (int i = Settings.SampleCount; i < GenableList.Count; i++)
                    {
                        GenableList[i].Brain.MutateFrom(GenableList[Random.Range(0, Settings.SampleCount)].Brain, Settings.MutationValue.Evaluate(Time.time - timeStart));
                    }

                    for (int i = 0; i < GenableList.Count; i++)
                    {
                        BaseGenable genable = GenableList[i];
                        ActionOnNewGeneration(ref genable, i);
                    }

                    yield return new WaitForSecondsRealtime(1);
                    Time.timeScale = 1;
                }
            }
        }

        #endregion private functions
    }

    [System.Serializable]
    public class GeneticSettings
    {
#pragma warning disable
        [SerializeField] private int populationCount;
        [SerializeField] private int populationTime;
        [SerializeField, Range(1f, 100f)] private float sampleFromPopulationPercent;
        [SerializeField] private AnimationCurve mutationValue;
#pragma warning restore

        public int PopulationCount => populationCount;
        public int PopulationTime => populationTime;
        public float SampleFromPopulationPercent => sampleFromPopulationPercent;
        public float SampleFromPopulation => sampleFromPopulationPercent / 100f;
        public int SampleCount => Mathf.Clamp((int)(PopulationCount * SampleFromPopulation), 1, PopulationCount);
        public AnimationCurve MutationValue => mutationValue;
    }
}

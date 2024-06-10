using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Global.Component.Genetic;
using Global.Component.Perceptron;
using Managers.Datas;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComputerControl : BaseGenable
{
    [SerializeField] private SpaceShipControl shipControl;

    [SerializeField] private Brain brain;
    [SerializeField] private float lifeCycleTime = 0.1f;

    public override float Value => shipControl.Money;

    public override Brain Brain => brain;

    public override void Refresh()
    {
        List<Brain> brains = Services.GetManager<OtherSpaceshipManager>().GetBests(shipControl);
        if (brains.Count > 2)
        {
            brain.MutateFrom(brains[Random.Range(0, brains.Count - 1)], brains[Random.Range(0, brains.Count - 1)], Services.GetManager<OtherSpaceshipManager>().GeneticValue);
            return;
        }
        brain.MutateFrom(Services.GetManager<OtherSpaceshipManager>().GetBest(shipControl), Services.GetManager<OtherSpaceshipManager>().GeneticValue);
    }

    private void Start()
    {
        Brain.CreateNewBrain();
        if (brain.HaveSavedData)
        {
            brain.FromStrData();
        }
        else
        {
            brain.SetRandom();
        }
        StartCoroutine(LifeCoroutine());
    }

    private void LifeCycle()
    {
        brain.RecalculateValues();
        float f = 0;
        float r = 0;
        f += brain.Layers[brain.Layers.Count - 1].neurons[0].Value;
        f = Mathf.Clamp(f, -1, 1);
        r += brain.Layers[brain.Layers.Count - 1].neurons[1].Value;
        r = Mathf.Clamp(r, -1, 1);
        shipControl.ControlFloatValues(f, r);

        if (brain.Layers[brain.Layers.Count - 1].neurons[2].Value > 0)
        {
            shipControl.Shoot();
        }

        if (Value < 1)
        {
            Services.GetManager<OtherSpaceshipManager>().RespawnMe(shipControl);
        }

    }

    private IEnumerator LifeCoroutine()
    {
        while (true)
        {
            LifeCycle();
            yield return new WaitForSeconds(lifeCycleTime);
        }
    }
}

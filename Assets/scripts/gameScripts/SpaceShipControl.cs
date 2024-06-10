using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Managers.Datas;
using UnityEngine;

public class SpaceShipControl : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;

    [SerializeField] private float forwardThrusterSpeed;
    [SerializeField] private float rotationThrusterSpeed;

    [SerializeField] private GameObject mainEngineVisual;
    [SerializeField] private GameObject rightEngineVisual;
    [SerializeField] private GameObject leftEngineVisual;

    [SerializeField] private Laser laserPrefab;
    [SerializeField] private int laserDamage;
    [SerializeField] private int hp = 15;
    [SerializeField] private int hpMax = 15;
    [SerializeField] private float healDelay = 1;
    [SerializeField] private int moneyFromDamage = 1;
    [SerializeField] private int laserKD = 4;

    [SerializeField] private AnimationCurve SpaceshipHitBonus;
    public AnimationCurve AsteroidHitBonus;

    public float Money = 500;
    public int SavedMoney = 500;
    public int hit;
    public float laserCost = 5;

    private OtherSpaceshipManager manager;

    public int HP
    {
        get => hp;
        set => hp = Mathf.Clamp(value, 0, hpMax);
    }

    private float forward;
    private float rotation;
    public Vector2Int quad;

    private void Start()
    {
        StartCoroutine(HealCoroutine());
        manager = Services.GetManager<OtherSpaceshipManager>();
    }
    private IEnumerator HealCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(healDelay);
            if (hp < hpMax)
            {
                TryHeal();
            }
        }
    }

    public void GiveDamage(SpaceShipControl otherShip)
    {
        hp -= otherShip.laserDamage;
        otherShip.Money += otherShip.moneyFromDamage;
        if (hp < 1)
        {
            otherShip.Money += (int)((Money + manager.BasicKill) * SpaceshipHitBonus.Evaluate(otherShip.hit) * manager.KillBonus);
            otherShip.HP += (int)(15 * SpaceshipHitBonus.Evaluate(otherShip.hit));
            manager.RespawnMe(this);
        }
    }

    private void TryHeal()
    {
        if (Money > 100)
        {
            Money--;
            hp++;
        }
    }

    public void ControlValues(int thrustForward, int thrustRotation)
    {
        forward = thrustForward;
        rotation = thrustRotation;
    }

    public void ControlFloatValues(float thrustForward, float thrustRotation)
    {
        forward = thrustForward;
        rotation = thrustRotation;
    }

    public void Shoot()
    {
        if (Money > laserCost && laserOff < 1)
        {
            Money -= laserCost;
            laserPrefab.ShootVirtual();
            laserOff = laserKD;
        }
    }

    private int laserOff = 0;

    private void FixedUpdate()
    {

        if (Money > 0)
        {
            rig.AddRelativeForce(Vector3.forward * forward * forwardThrusterSpeed);
            rig.AddRelativeTorque(Vector3.up * rotation * rotationThrusterSpeed);
            mainEngineVisual.SetActive(forward > 0);
            rightEngineVisual.SetActive(rotation > 0);
            leftEngineVisual.SetActive(rotation < 0);

            Money -= manager.PowerToCost(Mathf.Abs(forward) + Mathf.Abs(rotation)) * Time.fixedDeltaTime;
        }
        laserOff--;
        if (laserOff < 2)
        {
            laserPrefab.ClearRay();
        }
        if (laserOff < 0)
        {
            laserOff = 0;
        }

    }

    public void WasRespawned()
    {
        Money = SavedMoney;
        hp = hpMax;
        hit /= 2;
    }
}

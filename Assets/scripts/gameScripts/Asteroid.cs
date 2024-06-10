using System.Collections;
using System.Collections.Generic;
using Global;
using Managers.Datas;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int durability;

    [SerializeField] private int heal = 5;
    [SerializeField] private int moneyPerShoot = 10;

    public Vector2Int quad;

    public void GiveDamage(SpaceShipControl shipControl)
    {
        durability--;
        shipControl.Money += (int)(moneyPerShoot * shipControl.AsteroidHitBonus.Evaluate(shipControl.hit));
        shipControl.HP += (int)(heal * shipControl.AsteroidHitBonus.Evaluate(shipControl.hit));
        if (durability < 1)
        {
            Services.GetManager<AsteroidManager>().RespawnAsteroid(this);
        }
    }
}

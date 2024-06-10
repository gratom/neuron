using System;
using UnityEngine;
public class Laser : MonoBehaviour
{
    public SpaceShipControl parentShip;

    [SerializeField] private float laserDistance;
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private Material mat1;
    [SerializeField] private Material mat2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SpaceShipControl>(out SpaceShipControl spaceShip))
        {
            if (spaceShip != parentShip)
            {
                spaceShip.GiveDamage(parentShip);
            }
        }
        if (other.TryGetComponent<Asteroid>(out Asteroid asteroid))
        {
            asteroid.GiveDamage(parentShip);
        }
        Destroy(gameObject);
    }

    public void ShootVirtual()
    {
        laserLine.SetPosition(1, Vector3.zero);
        laserLine.material = parentShip.hit > 0 ? mat1 : mat2;
        if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit, laserDistance))
        {
            if (hit.transform.TryGetComponent<SpaceShipControl>(out SpaceShipControl spaceShip))
            {
                if (spaceShip != parentShip)
                {
                    spaceShip.GiveDamage(parentShip);
                    parentShip.hit++;
                }
            }
            if (hit.transform.TryGetComponent<Asteroid>(out Asteroid asteroid))
            {
                asteroid.GiveDamage(parentShip);
                parentShip.hit--;
            }
            laserLine.SetPosition(1, Vector3.forward * hit.distance / 0.3f);
            return;
        }
        laserLine.SetPosition(1, Vector3.forward * laserDistance / 0.3f);
    }

    public void ClearRay()
    {
        laserLine.SetPosition(1, Vector3.zero);
    }

}

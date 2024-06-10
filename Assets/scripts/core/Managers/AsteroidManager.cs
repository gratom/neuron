using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers.Datas
{
    public class AsteroidManager : BaseManager
    {
        public override Type ManagerType => typeof(AsteroidManager);

        [SerializeField] private Asteroid asteroidPrefab;
        [SerializeField] private Transform boundBottomLeft;
        [SerializeField] private Transform boundTopRight;

        [SerializeField] private int count;
        [SerializeField] private int size;

        private List<Asteroid> asteroids = new List<Asteroid>();

        public void StartWork()
        {
            Spawn();
        }

        private void Spawn()
        {
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Vector2Int quad = new Vector2Int(i, j);
                    asteroids.Add(Instantiate(asteroidPrefab, GetRndPos(quad), asteroidPrefab.transform.rotation));
                    asteroids[index].quad = quad;
                    index++;
                }
            }
        }

        public void RespawnAsteroid(Asteroid asteroid)
        {
            asteroid.durability = asteroidPrefab.durability;
            asteroid.transform.position = GetRndPos(asteroid.quad);
        }

        public Vector2 GetNearestAsteroid(Vector2 point)
        {
            float nearest = 1000;
            int nearestIndex = 0;
            for (int i = 0; i < asteroids.Count; i++)
            {
                float distance = Vector2.Distance(point, new Vector2(asteroids[i].transform.position.x, asteroids[i].transform.position.z));
                if (nearest > distance)
                {
                    nearest = distance;
                    nearestIndex = i;
                }
            }
            return new Vector2(asteroids[nearestIndex].transform.position.x, asteroids[nearestIndex].transform.position.z);
        }

        public Vector3 GetRndPos()
        {
            return new Vector3(Random.Range(boundBottomLeft.position.x, boundTopRight.position.x), 0, Random.Range(boundBottomLeft.position.z, boundTopRight.position.z));
        }

        public Vector3 GetRndPos(Vector2Int quad)
        {
            return new Vector3(Random.Range(boundBottomLeft.position.x, boundTopRight.position.x), 0, Random.Range(boundBottomLeft.position.z, boundTopRight.position.z)) + new Vector3(quad.x, 0, quad.y) * size;
        }

    }
}

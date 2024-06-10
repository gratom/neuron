using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class SelfDestroyer : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 3;

        private void Awake()
        {
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(timeToDestroy);
            Destroy(gameObject);
        }
    }
}
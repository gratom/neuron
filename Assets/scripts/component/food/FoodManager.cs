using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Food> foods;

    public Vector3 GetNearestFood(Vector3 position)
    {
        float minDist = float.MaxValue;
        int index = 0;
        float curDist;
        for (int i = 0; i < foods.Count; i++)
        {
            curDist = Vector3.Distance(position, foods[i].transform.position);
            if (curDist < minDist)
            {
                minDist = curDist;
                index = i;
            }
        }
        return foods[index].transform.position;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject healthPrefab;

    public void SetHealth(int amount)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < amount; i++)
        {
            Instantiate(healthPrefab, transform);
        }
    }
}

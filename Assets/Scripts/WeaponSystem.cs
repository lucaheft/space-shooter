using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject bulletPrefab;
    public float fireRate = 1f;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    public void Fire()
    {
        if (time >= fireRate)
        {
            time = 0;
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(bulletPrefab, spawnPoint);
            }
        }
    }
}

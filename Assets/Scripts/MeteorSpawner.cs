using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public Camera camera;
    public GameObject meteorPrefab;
    public float spawnRate = 1f;

    public float meteorMinSpeed = 0.5f;
    public float meteorMaxSpeed = 1.5f;
    public float meteorMinTorque = 0f;
    public float meteorMaxTorque = 50f;
    public GameObject player;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnRate)
        {
            time = 0;
            SpawnMeteor();
        }
    }

    void SpawnMeteor()
    {
        GameObject meteor = Instantiate(meteorPrefab);
        float xPos;
        float yPos;

        if (Random.Range(0, 2) == 0)
        {
            float halfWidth = camera.orthographicSize * camera.aspect;
            xPos = Random.Range(-halfWidth, halfWidth);
            float sign = Random.Range(0, 2) == 0 ? -1 : 1;
            yPos = (camera.orthographicSize + 1) * sign;
        }
        else
        {
            float halfHeight = camera.orthographicSize;
            yPos = Random.Range(-halfHeight, halfHeight);
            float sign = Random.Range(0, 2) == 0 ? -1 : 1;
            xPos = (camera.orthographicSize * camera.aspect + 1) * sign;
        }

        meteor.transform.position = new Vector2(xPos, yPos);
        Vector2 direction = player.transform.position - meteor.transform.position;
        float speed = Random.Range(meteorMinSpeed, meteorMaxSpeed);
        float torque = Random.Range(meteorMinTorque, meteorMaxTorque);

        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
        rb.AddTorque(torque);
    }
}

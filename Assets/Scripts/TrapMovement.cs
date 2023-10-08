using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMovement : MonoBehaviour
{
    float timer = 0.0f;
    float speed = 1.0f;
    public float maxTime = 5.0f;
    Vector3 movement = new Vector3 (0, 1, 0);

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > maxTime)
        {
            movement = -1.0f * movement;
            timer = 0.0f;
        }

        transform.position = transform.position + movement * Time.deltaTime * speed;
    }
}

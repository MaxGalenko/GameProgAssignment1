using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public GameObject particleSystem; 
    public float rotationSpeed = 45.0f;
    private float timer = 0.0f;
    private float speed = 0.1f;
    public float maxTime = 0.5f;
    Vector3 movement = new Vector3 (0, 1, 0);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if(timer > maxTime)
        {
            movement = -1.0f * movement;
            timer = 0.0f;
        }

        transform.position = transform.position + movement * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            GameManager.Instance.JumpPowerUpOn();
            gameObject.SetActive(false);
            GameObject particleSystemBurst = Instantiate(particleSystem, transform.position, transform.rotation);
            Invoke("ResetJumpPowerUp", 30.0f);
        }
    }
    
    private void ResetJumpPowerUp()
    {
        gameObject.SetActive(true);
    }
}

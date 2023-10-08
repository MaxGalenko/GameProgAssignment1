using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public GameObject particleSystem; 

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            GameManager.Instance.JumpPowerUpOn();
            gameObject.SetActive(false);
            GameObject particleSystemBurst = Instantiate(particleSystem, transform.position, transform.rotation);
            Destroy(particleSystemBurst, 3);
        }
    }
}

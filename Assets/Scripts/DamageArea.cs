using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    ParticleSystem puffEffect; 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colidiu com" + other.name);
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}

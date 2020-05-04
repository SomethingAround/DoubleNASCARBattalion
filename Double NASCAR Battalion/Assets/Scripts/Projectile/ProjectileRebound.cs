using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRebound : MonoBehaviour
{
    public float projectileSpeed = 10.0f;
    public GameObject currentProjectile;

    private int numberOfRebounds = 0;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.right * projectileSpeed;
        GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        numberOfRebounds += 1;

        if (numberOfRebounds >= 2)
            currentProjectile.gameObject.SetActive(false); 
    }
}

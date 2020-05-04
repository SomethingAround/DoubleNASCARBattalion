using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Transform m_turret;
	Rigidbody m_rb;
    // Start is called before the first frame update
    void Awake()
    {
		m_turret = gameObject.GetComponent<Transform>().parent;
		m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		print("Test");
    }
}

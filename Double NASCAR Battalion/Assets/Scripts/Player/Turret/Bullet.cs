using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[Tooltip("How much damage the player takes when hit")]
	public int m_bulletDamage = 1;

	[Tooltip("Speed of the Bullet")]
	public float m_bulletSpeed = 50.0f;

	[Tooltip("Use this to line the height up with the barrel of the turret \n" +
		"Note: This is relative to the turret's position")]
	public float m_fireHeight;

	[Tooltip("Offsets how far the rocket will fire from the centre of the turret")]
	public float m_rocketOffset;

	Vector3 m_distanceFromPivot;

	[HideInInspector]
	public bool m_fired = false;

	Transform m_ammoPool;

	[HideInInspector]
	public Transform m_turret;
	[HideInInspector]
	public PlayerController m_player;
	[HideInInspector]
	public Collider m_collider;

	Rigidbody m_rb;
    
	void Awake()
	{
		m_distanceFromPivot = new Vector3(0, m_fireHeight, 0);
		m_ammoPool = gameObject.GetComponent<Transform>().parent;
		m_rb = GetComponent<Rigidbody>();
		m_collider = gameObject.GetComponent<Collider>();
		gameObject.transform.localPosition = Vector3.zero;
	}

	public void OnFired()
	{
		m_rb.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.LookRotation(m_turret.forward);
		transform.position = m_turret.position + m_distanceFromPivot + (m_turret.forward * m_rocketOffset);
		m_rb.velocity = m_turret.forward * m_bulletSpeed;
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Bullet")
		{
			gameObject.SetActive(false);
		}
		if(other.gameObject.tag == "Player" && other.gameObject != m_player.gameObject)
		{
			gameObject.SetActive(false);
			other.gameObject.GetComponent<PlayerController>().health -= m_bulletDamage;
		}
		gameObject.SetActive(false);
	}
}

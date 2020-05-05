using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[Tooltip("Speed of the Bullet")]
	public float m_bulletSpeed = 50.0f;

	public Vector3 m_spawnDistanceFromTurretPivot = new Vector3(0, 5.9f, 0);

	

	Transform m_ammoPool;
	//[Tooltip("The turret that the ammo will be shot from \n" +
	//	"Note: This will be used at a later time as need to " +
	//	"find out how to get certain turrets for each player")]
	
	public Transform m_turret;

	Collider m_collider;

	Rigidbody m_rb;
    
	void Awake()
	{
		m_spawnDistanceFromTurretPivot = new Vector3(0, 5.9f, 0);
		gameObject.transform.localPosition = Vector3.zero;
	}

    void OnEnable()
    {

		transform.rotation = Quaternion.LookRotation(m_turret.forward);
		transform.position = m_turret.position + m_spawnDistanceFromTurretPivot + (m_turret.forward * 5.75f);
		m_ammoPool = gameObject.GetComponent<Transform>().parent;
		m_rb = GetComponent<Rigidbody>();
		m_collider = gameObject.GetComponent<Collider>();
		m_rb.velocity = m_turret.forward * m_bulletSpeed;
	}

    // Update is called once per frame
    void Update()
    {

    }
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject == m_ammoPool.gameObject)
		{
			Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), m_collider);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Turret : MonoBehaviour
{
	//[HideInInspector]
	public bool m_rapidFire = false;

	[Tooltip("Which controller the player will be")]
	public int m_playerID = 2;
	[Tooltip("Amount of shots before reloading")]
	public int m_magSize = 3;

	static int m_ammoCount = 9;
	int m_ammoFired;
	int m_shotFired;


	[Tooltip("How long it takes to reload")]
	public float m_reloadTime = 3.00f;
	[Tooltip("How long it takes to fire the next shot")]
	public float m_shotDelay = 0.3f;
	[Tooltip("How long it takes to reload with the rapidfire pickup")]
	public float m_RapidfireReloadTime = 1.00f;
	[Tooltip("Delay between in shot with rapidfire pick up")]
	public float m_rapidfireShotDelay = 0.1f;
	[Tooltip("How long the rapidfire lasts for")]
	public float m_rapidfireTime = 8.0f;

	//Timer for the reload
	float m_reloadTimer;
	float m_shotTimer;
	float m_defaultReloadTime;
	float m_defaultShotDelay;
	float m_rapidfireTimer;

	Vector3 m_lookDirection = Vector3.zero;

	public bool m_gameActive;

	[Tooltip("Pass in the bullet prefab")]
	public Object m_bullet;

	[Tooltip("Pass in the game object that will store the ammo " +
		"Recommended: Seperate object from players so bullets will not rotate with the turret")]
	public Transform m_ammoPool;

	GameObject[] m_objectArray = new GameObject[m_ammoCount];
    // Start is called before the first frame update
    void Awake()
    {
		//Create the bullets
        for(int i = 0; i < m_ammoCount; ++i)
		{
			m_objectArray[i] = Instantiate(m_bullet, m_ammoPool) as GameObject;
			m_objectArray[i].SetActive(false);

			//Check if they have the scripts to set variables
			if(m_objectArray[i].TryGetComponent(out Bullet bullet))
			{
				bullet.m_turret = gameObject.transform;
				bullet.m_player = gameObject.GetComponentInParent<PlayerController>();
				Physics.IgnoreCollision(gameObject.GetComponentInParent<Collider>(), bullet.m_collider);
			}
		}
		m_defaultReloadTime = m_reloadTime;
		m_defaultShotDelay = m_shotDelay;
	}

	void Start()
	{
		//Set up the ignored Collisions
		for(int i = 0; i < m_ammoCount; ++i)
		{
			for ( int j = 0; j < m_ammoCount; ++j)
			{
				Physics.IgnoreCollision(m_objectArray[i].GetComponent<Collider>(), m_objectArray[j].GetComponent<Collider>());
			}
		}
	}

    //Update is called once per frame
    void Update()
	{
		if (m_gameActive)
		{
			//Checks if a controller is plugged in
			if (XCI.IsPluggedIn(m_playerID))
			{

				//Checks if the ammount of ammo fired is not equal to the mag size
				if (m_ammoFired != m_magSize)
				{

					//Checks if the shot timer is greater then shot delay so the player can shoot again
					if (m_shotTimer >= m_shotDelay)
					{

						//Checks if the right bumper is pressed down
						if (XCI.GetButtonDown(XboxButton.RightBumper, (XboxController)m_playerID))
						{
							m_objectArray[m_shotFired].SetActive(true);
							m_objectArray[m_shotFired].GetComponent<Bullet>().OnFired();
							++m_ammoFired;
							++m_shotFired;
							m_shotTimer = 0.0f;
							print("Fired " + m_ammoFired);
						}
					}
					else
					{
						m_shotTimer += Time.deltaTime;
					}
				}
				else
				{
					m_reloadTimer += Time.deltaTime;

					//Checks if the reload timer is greater then reload time
					if (m_reloadTimer >= m_reloadTime)
					{
						m_ammoFired = 0;
						m_reloadTimer = 0;

						//Checks if the shots fired is equal to the ammo count
						if (m_shotFired == m_ammoCount)
						{
							m_shotFired = 0;
						}
					}
				}

				//Checks if the right analog stick is moved
				if (XCI.GetAxis(XboxAxis.RightStickX, (XboxController)m_playerID) != 0 || XCI.GetAxis(XboxAxis.RightStickY, (XboxController)m_playerID) != 0)
				{
					m_lookDirection.x = XCI.GetAxis(XboxAxis.RightStickX, (XboxController)m_playerID);
					m_lookDirection.z = XCI.GetAxis(XboxAxis.RightStickY, (XboxController)m_playerID);
					transform.rotation = Quaternion.LookRotation(m_lookDirection);
				}
			}
			//Checks if the rapidfire bool is true
			if (m_rapidFire)
			{
				m_rapidfireTimer += Time.deltaTime;
				m_shotDelay = m_rapidfireShotDelay;
				m_reloadTime = m_RapidfireReloadTime;
			}

			//Checks if the timer is greater then the time for the pickup
			if (m_rapidfireTimer >= m_rapidfireTime)
			{
				m_shotDelay = m_defaultShotDelay;
				m_reloadTime = m_defaultReloadTime;
				m_rapidFire = false;
				m_rapidfireTimer = 0.0f;
			}
		}
	}
}

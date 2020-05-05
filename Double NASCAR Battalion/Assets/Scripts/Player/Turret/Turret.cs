using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Turret : MonoBehaviour
{
	[HideInInspector]
	public bool m_rapidFire = false;

	[Tooltip("Which controller the player will be")]
	public int m_playerID = 2;

	static int m_ammoCount = 3;
	int m_ammoFired;


	[Tooltip("How long it takes to reload")]
	public float m_reloadTime = 3.00f;
	[Tooltip("How long it takes to reload while rapidfire is active")]
	public float m_rapidFireReloadTime = 1.00f;

	public float m_shotDelay = 0.3f;
	//Timer for the reload
	float m_reloadTimer;
	float m_shotTimer;

	Vector3 m_lookDirection = Vector3.zero;

	[Tooltip("Pass in the bullet prefab")]
	public Object m_bullet;

	[Tooltip("Pass in the game object that will store the ammo " +
		"Recommended: Seperate object from players so bullets will not rotate with the turret")]
	public Transform m_ammoPool;

	GameObject[] m_objectArray = new GameObject[m_ammoCount];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < m_ammoCount; ++i)
		{
			m_objectArray[i] = Instantiate(m_bullet, m_ammoPool) as GameObject;
			m_objectArray[i].SetActive(false);
			if(m_objectArray[i].TryGetComponent(out Bullet bullet))
			{
				bullet.m_turret = gameObject.transform;
			}
		}
    }

    // Update is called once per frame
    void Update()
	{
		if (XCI.IsPluggedIn(m_playerID))
		{
			if (m_ammoFired != m_ammoCount)
			{
				if (m_shotTimer >= m_shotDelay)
				{
					if (XCI.GetButtonDown(XboxButton.RightBumper, (XboxController)m_playerID))
					{
						m_objectArray[m_ammoFired].SetActive(true);
						++m_ammoFired;
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
				if (m_reloadTimer >= m_reloadTime)
				{
					print("Reloading");
					m_ammoFired = 0;
					m_reloadTimer = 0;
					for(int i = 0; i < m_ammoCount; ++i)
					{
						if (m_objectArray[i].activeSelf == true)
						{
							m_objectArray[i].SetActive(false);
						}
					}
				}
			}
			if(XCI.GetAxis(XboxAxis.RightStickX, (XboxController)m_playerID) != 0 || XCI.GetAxis(XboxAxis.RightStickY, (XboxController)m_playerID) != 0)
			{
				m_lookDirection.x = XCI.GetAxis(XboxAxis.RightStickX, (XboxController)m_playerID);
				m_lookDirection.z = XCI.GetAxis(XboxAxis.RightStickY, (XboxController)m_playerID);
				transform.rotation = Quaternion.LookRotation(m_lookDirection);
			}
		}
		print(m_shotTimer);
    }
}

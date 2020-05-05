using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Turret : MonoBehaviour
{

	static int m_ammo = 3;

	int m_ammoFired;

	public int m_playerID = 2;

	//Timer for the reload
	float m_reloadTimer;
	//How long it takes to reload
	public float m_reloadTime = 3.00f;

	Vector3 m_lookDirection = Vector3.zero;

	//Pass in the bullet prefab
	public Object m_bullet;

	//Pass in the turret game object
	public Transform m_turret;

	GameObject[] m_objectArray = new GameObject[m_ammo];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < m_ammo; ++i)
		{
			m_objectArray[i] = Instantiate(m_bullet, m_turret) as GameObject;
			m_objectArray[i].SetActive(false);
		}
    }

    // Update is called once per frame
    void Update()
	{
		if (XCI.IsPluggedIn(m_playerID))
		{
			if (m_ammoFired != m_ammo)
			{
				if (XCI.GetButtonDown(XboxButton.RightBumper, (XboxController)m_playerID))
				{
					m_objectArray[m_ammoFired].SetActive(true);
					++m_ammoFired;
					print("Fired " + m_ammoFired);
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
				}
			}
			if(XCI.GetAxis(XboxAxis.RightStickX, (XboxController)m_playerID) != 0 || XCI.GetAxis(XboxAxis.RightStickY, (XboxController)m_playerID) != 0)
			{
				m_lookDirection.x = XCI.GetAxis(XboxAxis.RightStickX, (XboxController)m_playerID);
				m_lookDirection.z = XCI.GetAxis(XboxAxis.RightStickY, (XboxController)m_playerID);
				transform.rotation = Quaternion.LookRotation(m_lookDirection);
			}
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int playerID = 4;
    public float playerSpeed = 5;
	[Tooltip("Amount of health the player can have")]
	public int maxHealth = 4;
	[Tooltip("AMount of health the player gets back on pick up")]
	public int healthOnPickup = 1;
	[HideInInspector]
	public int health;

	float healthTimer;
	[Tooltip("Time till the health pickup respawns")]
	public float healthTime = 3.0f;
	float rapidfireTimer;
	[Tooltip("Time till the rapidfire pickup respawns")]
	public float rapidfireTime = 3.0f;

	Vector3 startPosition;

	bool healthPickedUped;

	bool rapidfirePickedUped;

	GameObject healthPickUp;

	GameObject rapidfirePickUp;

	Turret turret;

    private Rigidbody rb;

	public int points;
	Text pointsText;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
		turret = gameObject.GetComponentInChildren<Turret>();
		health = maxHealth;
		startPosition = transform.position;
		points = 0;
		pointsText.text = points.ToString();
    }

    private void Update()
    {
        if (XCI.IsPluggedIn(playerID))
        {
            if (XCI.GetAxis(XboxAxis.LeftStickX, (XboxController)playerID) != 0 || XCI.GetAxis(XboxAxis.LeftStickY, (XboxController)playerID) != 0)
            {
                Vector3 inputDirection = new Vector3(XCI.GetAxis(XboxAxis.LeftStickX, (XboxController)playerID), 0, XCI.GetAxis(XboxAxis.LeftStickY, (XboxController)playerID));
                inputDirection.Normalize();

                rb.transform.localRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
                rb.velocity = transform.forward * playerSpeed;
            }
        }

		if(health > maxHealth)
		{
			health = maxHealth;
		}

		if(rapidfirePickedUped)
		{
			rapidfireTimer += Time.deltaTime;
		}

		if(healthPickedUped)
		{
			healthTimer += Time.deltaTime;
		}

		if(healthTimer >= healthTime)
		{
			healthPickedUped = false;
			healthPickUp.SetActive(true);
			healthTimer = 0.0f;
		}

		if (rapidfireTimer >= rapidfireTime)
		{
			rapidfirePickedUped = false;
			rapidfirePickUp.SetActive(true);
			rapidfireTimer = 0.0f;
		}

		if(health <= 0)
		{
			transform.position = startPosition;
			rb.velocity = Vector3.zero;
			health = 4;
			pointsText.text = points.ToString();
		}

		rb.angularVelocity = Vector3.zero;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Health")
		{
			health += healthOnPickup;
			healthPickUp = other.gameObject;
			healthPickedUped = true;
			other.gameObject.SetActive(false);

		}
		if(other.gameObject.tag == "Rapid Fire")
		{
			turret.m_rapidFire = true;
			rapidfirePickUp = other.gameObject;
			rapidfirePickedUped = true;
			other.gameObject.SetActive(false);
		}
	}
}

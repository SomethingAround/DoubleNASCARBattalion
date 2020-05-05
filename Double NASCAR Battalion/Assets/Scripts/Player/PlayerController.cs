using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{
    public int playerID = 4;
    public float playerSpeed = 5;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float maxForce;


    void Update()
    {
        if (Input.GetButtonDown("Strike"))
        {
            timePressed = Time.time;
        }
        if (Input.GetButtonUp("Strike"))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * maxForce, ForceMode.VelocityChange);
            timePressed = 0;
        }
    }

    private float timePressed = 0;
}

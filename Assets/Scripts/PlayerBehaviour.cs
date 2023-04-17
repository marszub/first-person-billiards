using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    public float maxForce;
    public float maxTime;
    public float zeroVelocity;
    public PowerBar powerBar;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        powerBar.setFillAmount(getForcePercent());
        if(rigid.velocity.magnitude > zeroVelocity)
        {
            return;
        }
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        if (Input.GetButtonDown("Strike"))
        {
            timePressed = Time.time;
        }
        if (Input.GetButtonUp("Strike"))
        {
            Debug.Log("Strike");
            Vector3 direction = Camera.main.transform.forward;
            if(direction.y < 0)
            {
                direction.y = 0.0f;
            }
            rigid.AddForce(direction * maxForce * getForcePercent(), ForceMode.VelocityChange);
            timePressed = invalidTime;
        }
        if (Input.GetButtonDown("CancelStrike"))
        {
            timePressed = invalidTime;
        }
    }

    public float getForcePercent()
    {
        if(timePressed == invalidTime)
        {
            return 0.0f;
        }
        return Mathf.Min(1, (Time.time - timePressed) / maxTime);
    }

    private float timePressed = invalidTime;
    private const float invalidTime = -1.0f;

    private Rigidbody rigid;
}

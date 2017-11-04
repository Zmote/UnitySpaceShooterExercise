using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeController : MonoBehaviour
{
    public float tilt;
    public float speed;
    public float evasionDelay;
    public float evasionDuration;
    private float nextEvasion;
    public Boundary boundary;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        nextEvasion = Time.time + evasionDelay;
    }

    void Update()
    {
        if(Time.time >= nextEvasion)
        {
            Evade();
            if(Time.time > nextEvasion + evasionDuration)
            {
                nextEvasion = Time.time + evasionDelay;
            }
        }
    }

    void Evade()
    {
        float directionalSpeed = speed * -Mathf.Sign(transform.position.x / Mathf.Abs(transform.position.x));
        rigidBody.velocity = new Vector3(rigidBody.velocity.x + (directionalSpeed * Time.deltaTime), rigidBody.velocity.y, rigidBody.velocity.z);
        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
            );

        rigidBody.rotation = Quaternion.Euler(0.0f, -180.0f, rigidBody.velocity.x * tilt);
    }
}

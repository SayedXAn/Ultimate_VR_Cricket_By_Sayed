using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool hitByBat = false;
    public float bounceMultiplier = 500f;

    private Vector3 ballVelocity;

    public TrailRenderer trail;


    public float speed = 0;
    public float spinTorque = 5f;
    public Vector3 spinAxis = Vector3.zero;

    Vector3 lastPosition = Vector3.zero;
    private bool hasBounced = false;
    public float deviationValue = 13f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(CountDownTimer());
    }
    void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }    

    void Update()
    {
        if (hitByBat && rb.linearVelocity.magnitude <= 0.0008f)
        {
            rb.isKinematic = true;
        }

        //if(hitByBat && speed <= 0.0008f)
        //{
        //    GetComponent<Rigidbody>().isKinematic = true;
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.CompareTag("bat"))
        //{
        //    hitByBat = true;
        //    trail.emitting = true;
        //}
        if (collision.gameObject.CompareTag("pitch") && !hitByBat && !hasBounced)
        {
            hasBounced = true;

            // Apply bounce deviation (side movement)
            Vector3 velocity = rb.linearVelocity;
            float deviation = Random.Range(-deviationValue, deviationValue);
            velocity += transform.forward * deviation;
            rb.linearVelocity = velocity;

            // Now apply spin torque after bounce
            if (spinAxis != Vector3.zero && spinTorque > 0f)
            {
                rb.AddTorque(spinAxis.normalized * spinTorque, ForceMode.Impulse);
            }
        }

    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }
    
}

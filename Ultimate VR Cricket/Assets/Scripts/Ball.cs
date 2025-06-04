using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool hitByBat = false;
    public float bounceMultiplier = 500f;

    private Vector3 ballVelocity;

    private int lifeTime = 15;
    public TrailRenderer trail;

    public float speed = 0;

    Vector3 lastPosition = Vector3.zero;

    void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    void Update()
    {
        
        if(hitByBat && speed <= 0.0008f)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bat"))
        {
            hitByBat = true;
            trail.emitting = true;
        }

        //if (collision.gameObject.CompareTag("pitch"))
        //{
        //    Rigidbody ballRb = GetComponent<Rigidbody>();
        //    if (ballRb != null && !hitByBat)
        //    {
        //        Vector3 newVelocity = ballRb.linearVelocity;
        //        newVelocity.y = Mathf.Abs(newVelocity.y) * bounceMultiplier;
        //        ballRb.linearVelocity = newVelocity;
        //    }
        //}
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(1f);
        lifeTime--;
        if(lifeTime == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(CountDownTimer());
        }
    }
    
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public bool hitByBat = false;
    public bool hitGround = false;
    public bool hitBoundary = false;
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
    ScoreManager scoreManager;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreManager = GameObject.FindWithTag("logics").GetComponent<ScoreManager>();
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
        if (hitByBat)
        {
            Debug.DrawRay(transform.position, Vector3.up * 0.2f, Color.green);
        }
        //if(hitByBat && speed <= 0.0008f)
        //{
        //    GetComponent<Rigidbody>().isKinematic = true;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bat"))
        {
            hitByBat = true;
            //trail.emitting = true; //Always on kore disi
        }
        if (hitByBat && collision.gameObject.CompareTag("pitch"))
        {
            hitGround = true;
        }
        if (hitByBat && collision.gameObject.CompareTag("field"))
        {
            hitGround = true;
        }
        if(collision.gameObject.CompareTag("stamp"))
        {
            //out
            scoreManager.UpdateScore(0, 1);
        }
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

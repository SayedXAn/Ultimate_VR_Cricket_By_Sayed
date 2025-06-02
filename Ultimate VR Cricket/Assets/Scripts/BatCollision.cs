using UnityEngine;
public class BatCollision : MonoBehaviour
{
    public float powerMultiplier = 1.5f;
    private Vector3 lastPosition;
    private Vector3 batVelocity;
    public AudioSource AS;

    void Update()
    {
        // Track bat velocity manually
        batVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            AS.Play();
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                // Add bat�s velocity to the ball
                ballRb.linearVelocity += batVelocity * powerMultiplier;
            }
        }
    }
}

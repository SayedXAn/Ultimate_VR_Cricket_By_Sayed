using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float bounceMultiplier = 1.0f;
    private bool hasBounced = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("pitch") && !hasBounced)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 velocity = rb.linearVelocity;
                velocity.y = Mathf.Abs(velocity.y) * bounceMultiplier;
                rb.linearVelocity = velocity;
                hasBounced = true;
            }
        }
    }
}

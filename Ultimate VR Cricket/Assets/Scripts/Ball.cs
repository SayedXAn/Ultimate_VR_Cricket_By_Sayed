using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool hitByBat = false;
    public float bounceMultiplier = 500f;

    private Vector3 lastPosition;
    private Vector3 ballVelocity;

    private int lifeTime = 15;

    private void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    void Update()
    {
        ballVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bat"))
        {
            hitByBat = true;
        }

        if (collision.gameObject.CompareTag("pitch"))
        {
            Rigidbody ballRb = GetComponent<Rigidbody>();
            if (ballRb != null && !hitByBat)
            {
                Vector3 newVelocity = ballRb.linearVelocity;
                newVelocity.y = Mathf.Abs(newVelocity.y) * bounceMultiplier;
                ballRb.linearVelocity = newVelocity;
            }
        }
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

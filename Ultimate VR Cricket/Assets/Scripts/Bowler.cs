using UnityEngine;

public class Bowler : MonoBehaviour
{
    [Header("Bowling Setup")]
    public GameObject ballPrefab;
    public GameObject bowlingPoint;
    public Transform dropTarget;         // First bounce point
    public float timeToDrop = 1.2f;      // Time to first bounce
    public float deliveryInterval = 2f;
    public GameObject initPos;
    public string animationName = "Bowling_Anim";// Time between deliveries

    [Header("Variation")]
    public float angleVariation = 5f;       // Degrees left/right
    public float spinTorque = 5f;           // Spin on delivery
    public float bounceRandomness = 0.15f;
    Ball ballScript;// 0.1 = �10% bounce height variation


    private float timer = 0f;

    private void Start()
    {
        
        InvokeRepeating("StartBowling", 1f, 8f);
    }

    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer >= deliveryInterval)
        //{
        //    timer = 0f;
        //    Bowl();
        //}
    }

    public void Bowl()
    {
        dropTarget.transform.position = new Vector3(Random.Range(26f, 34f), dropTarget.transform.position.y, dropTarget.transform.position.z);
        GameObject ball = Instantiate(ballPrefab, bowlingPoint.transform.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Ball prefab must have a Rigidbody.");
            return;
        }

        // Direction variation
        Vector3 targetPos = dropTarget.position;
        float angleOffset = Random.Range(-angleVariation, angleVariation);
        Vector3 lateralOffset = Quaternion.Euler(0, angleOffset, 0) * (targetPos - transform.position);
        targetPos = transform.position + lateralOffset;

        // Compute velocity
        timeToDrop = Random.Range(0.75f, 1.3f);
        Vector3 velocity = CalculateLaunchVelocity(transform.position, targetPos, timeToDrop);
        rb.linearVelocity = velocity;

        // Define spin direction (e.g., leg spin = right, off spin = -right)
        Vector3 spinAxis = Vector3.right;

        // Send spin data to Ball script
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.spinTorque = spinTorque;
            ballScript.spinAxis = spinAxis;
        }
    }

    Vector3 CalculateLaunchVelocity(Vector3 origin, Vector3 target, float time)
    {
        Vector3 toTarget = target - origin;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);

        float gravity = Mathf.Abs(Physics.gravity.y);
        float vx = toTargetXZ.magnitude / time;
        float vy = (toTarget.y + 0.5f * gravity * time * time) / time;

        Vector3 result = toTargetXZ.normalized * vx;
        result.y = vy;

        return result;
    }

    public void ResetBowler()
    {
        //Debug.Log("Dhukse");
        GetComponent<Animator>().applyRootMotion = false;
        //GetComponent<Animator>().enabled = false;
        //GetComponent<Animator>().runtimeAnimatorController = idleCon;
        transform.position = initPos.transform.position;
    }

    public void StartBowling()
    {
        //GetComponent<Animator>().runtimeAnimatorController = bowlingCon;
        //Debug.Log("Starttt");
        //GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().applyRootMotion = true;
        GetComponent<Animator>().Play(animationName, -1, 0f);

    }


    public void LegSpinBowl()
    {
        dropTarget.transform.position = new Vector3(Random.Range(26f, 34f), dropTarget.transform.position.y, dropTarget.transform.position.z);
        GameObject ball = Instantiate(ballPrefab, bowlingPoint.transform.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Ball prefab must have a Rigidbody.");
            return;
        }

        // Direction variation
        Vector3 targetPos = dropTarget.position;
        float angleOffset = Random.Range(0f, angleVariation);
        Vector3 lateralOffset = Quaternion.Euler(0, angleOffset, 0) * (targetPos - transform.position);
        targetPos = transform.position + lateralOffset;

        // Compute velocity
        timeToDrop = Random.Range(1.1f, 1.3f);
        Vector3 velocity = CalculateLaunchVelocity(transform.position, targetPos, timeToDrop);
        rb.linearVelocity = velocity;

        // Define spin direction (e.g., leg spin = right, off spin = -right)
        Vector3 spinAxis = Vector3.right;

        // Send spin data to Ball script
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.spinTorque = spinTorque;
            ballScript.spinAxis = spinAxis;
        }
    }

    public void OffSpinBowl()
    {
        dropTarget.transform.position = new Vector3(Random.Range(26f, 34f), dropTarget.transform.position.y, dropTarget.transform.position.z);
        GameObject ball = Instantiate(ballPrefab, bowlingPoint.transform.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Ball prefab must have a Rigidbody.");
            return;
        }

        // Direction variation
        Vector3 targetPos = dropTarget.position;
        float angleOffset = Random.Range(-angleVariation, angleVariation);
        Vector3 lateralOffset = Quaternion.Euler(0, angleOffset, 0) * (targetPos - transform.position);
        targetPos = transform.position + lateralOffset;

        // Compute velocity
        timeToDrop = Random.Range(1.1f, 1.3f);
        Vector3 velocity = CalculateLaunchVelocity(transform.position, targetPos, timeToDrop);
        rb.linearVelocity = velocity;

        // Define spin direction (e.g., leg spin = right, off spin = -right)
        Vector3 spinAxis = Vector3.right;

        // Send spin data to Ball script
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.spinTorque = spinTorque;
            ballScript.spinAxis = spinAxis;
        }
    }


}


//
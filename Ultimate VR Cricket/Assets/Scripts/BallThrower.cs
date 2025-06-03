using UnityEngine;

public class BallThrower : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPoint;      // where the bowler “releases” the ball
    public Transform target;          // e.g., the batter’s off-stump
    public GameObject ballPrefab;     // prefab must carry a Rigidbody

    [Header("Timing")]
    [Min(0.1f)] public float interval = 2f;   // seconds between deliveries

    [Header("Pace (m/s)")]
    public float minSpeed = 25f;      // 90 km/h
    public float maxSpeed = 33f;      // 120 km/h

    [Header("Line / Length Variation (metres)")]
    public float bounceDistance = 5.5f;
    public float lateralSpread = 0.25f;   // ±25 cm either side
    public float lengthSpread = 0.35f;   // ±35 cm shorter/ fuller

    // ─────────────────────────────────────────────────────────────────────────────
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer -= interval;
            BowlOne();
        }
    }

    // ─────────────────────────────────────────────────────────────────────────────
    //void BowlOne()
    //{
    //    // 1. Instantiate ball
    //    GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
    //    Rigidbody rb = ball.GetComponent<Rigidbody>();

    //    // 2. Pick a random line & length around the nominal target
    //    Vector3 pitchDir = FlatDirection(spawnPoint.position, target.position); // X-Z only
    //    Vector3 sideDir = Vector3.Cross(Vector3.up, pitchDir).normalized;      // “around the wicket”

    //    Vector3 offset = sideDir * Random.Range(-lateralSpread, lateralSpread)
    //                   + pitchDir * Random.Range(-lengthSpread, lengthSpread);

    //    Vector3 aimPoint = target.position + offset;

    //    // 3. Choose a random pace, then compute the ballistic launch velocity
    //    float chosenSpeed = Random.Range(minSpeed, maxSpeed);
    //    Vector3 launchVel = SolveBallisticVelocity(spawnPoint.position, aimPoint, chosenSpeed);

    //    // 4. Fire!
    //    rb.linearVelocity = launchVel;
    //}
    void BowlOne()
    {
        // 1. Instantiate the ball
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // 2. Pick a random line & length to determine the bounce point (not target yet!)
        Vector3 pitchDir = FlatDirection(spawnPoint.position, target.position); // X-Z only
        Vector3 sideDir = Vector3.Cross(Vector3.up, pitchDir).normalized;

        Vector3 offset = sideDir * Random.Range(-lateralSpread, lateralSpread)
                       + pitchDir * Random.Range(-lengthSpread, lengthSpread);

        // 🏏 New: Set a bounce point instead of aiming directly at the batter
         // meters from bowler — adjust for realism
        Vector3 bouncePoint = spawnPoint.position + pitchDir * bounceDistance + offset;

        // 3. Choose a realistic speed toward the bounce point
        float chosenSpeed = Random.Range(minSpeed, maxSpeed);
        Vector3 launchVel = SolveBallisticVelocity(spawnPoint.position, bouncePoint, chosenSpeed);

        // 4. Fire!
        rb.linearVelocity = launchVel;
    }


    // ─────────────────────────────────────────────────────────────────────────────
    /// Returns the unit vector from a→b on the X-Z plane (ignores height).
    static Vector3 FlatDirection(Vector3 a, Vector3 b)
    {
        Vector3 flat = new Vector3(b.x - a.x, 0f, b.z - a.z);
        return flat.normalized;
    }

    /// <summary>
    /// Calculates an initial velocity that hits <paramref name="targetPos"/> when fired
    /// from <paramref name="origin"/> at the supplied speed.  If the target is out of range
    /// for that speed, it will lob as far as it can instead of erroring.
    /// </summary>
    Vector3 SolveBallisticVelocity(Vector3 origin, Vector3 targetPos, float speed)
    {
        Vector3 toTarget = targetPos - origin;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);
        float distanceXZ = toTargetXZ.magnitude;
        float heightDiff = toTarget.y;
        float g = Mathf.Abs(Physics.gravity.y);

        float speed2 = speed * speed;
        float discriminant = speed2 * speed2 - g * (g * distanceXZ * distanceXZ + 2 * heightDiff * speed2);

        // If the chosen speed can't reach, fall back to a flat throw
        if (discriminant < 0f)
            return toTargetXZ.normalized * speed + Vector3.up * 2f;  // hand-wave upward a bit

        float sqrtDisc = Mathf.Sqrt(discriminant);

        // Lower-angle solution is the more “bowler-like” trajectory
        float tanTheta = (speed2 - sqrtDisc) / (g * distanceXZ);
        float cosTheta = 1f / Mathf.Sqrt(1f + tanTheta * tanTheta);
        float sinTheta = tanTheta * cosTheta;

        Vector3 dirXZ = toTargetXZ.normalized;
        Vector3 result = dirXZ * speed * cosTheta + Vector3.up * speed * sinTheta;
        return result;
    }
}

using UnityEngine;

[RequireComponent(typeof(Transform))]
public class arrow_rotation : MonoBehaviour
{
    public Transform player;       // if left null, will pick Camera.main in Start()
    public Transform target;       // what the arrow should point to
    public float rotationSpeedDegPerSec = 180f; // degrees per second (smoothness)
    public float radius = 1f;      // distance from player
    public float heightOffset = 0f; // vertical offset from player's Y
    public bool stopWhenAligned = false; // optional: stop moving when aligned
    public float angleThresholdDeg = 1f; // used if stopWhenAligned == true

    private bool done = false;

    void Start()
    {
        if (player == null && Camera.main != null) player = Camera.main.transform;

        // if user didn't set a radius, keep the current distance at start
        if (player != null && radius <= 0.0001f)
            radius = Vector3.Distance(transform.position, player.position);


    }

    void Update()
    {
        if (player == null || target == null || done) return;

        // direction from player to target on XZ plane
        Vector3 toTarget = target.position - player.position;
        toTarget.y = 0f;
        if (toTarget.sqrMagnitude < 1e-6f) return;

        // target angle around player (degrees)
        float targetAngle = Mathf.Atan2(toTarget.z, toTarget.x) * Mathf.Rad2Deg;

        // current angle around player (degrees)
        Vector3 toArrow = transform.position - player.position;
        toArrow.y = 0f;
        float currentAngle = Mathf.Atan2(toArrow.z, toArrow.x) * Mathf.Rad2Deg;

        // smoothly move the angle (frame-rate independent)
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeedDegPerSec * Time.deltaTime);

        // set new position on the horizontal circle (keep Y locked)
        float rad = newAngle * Mathf.Deg2Rad;
        Vector3 newPos = player.position + new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)) * radius;
        newPos.y = player.position.y + heightOffset;
        transform.position = newPos;

        // point horizontally at the target (no pitch)
        Vector3 lookDir = target.position - transform.position;
        lookDir.y = 0f;
        if (lookDir.sqrMagnitude > 1e-6f)
        {
           // transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
            // If your arrow model faces +X (right) instead of +Z (forward),
             transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up) * Quaternion.Euler(90f, -90f, 0f);
        }

        // optionally stop once aligned
        if (stopWhenAligned)
        {
            float diff = Mathf.DeltaAngle(newAngle, targetAngle);
            if (Mathf.Abs(diff) <= angleThresholdDeg) done = true;
        }
    }
}
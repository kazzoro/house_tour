using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    private Transform target; // The object to follow


    private void Start()
    {
        if (target == null && Camera.main != null) target = Camera.main.transform;
    }
    void Update()
    {
        if (target != null)
        {
            // Get this object's current rotation
            Vector3 euler = transform.eulerAngles;

            // Replace only the Y axis with target's Y
            euler.y = target.eulerAngles.y;

            // Apply it back
            transform.eulerAngles = euler;
        }
    }
}

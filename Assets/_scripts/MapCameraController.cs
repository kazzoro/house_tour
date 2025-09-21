using UnityEngine;

public class MapCameraController : MonoBehaviour
{
    public float sensitivity = 2.0f;   // rotation speed
    public float smooth = 5.0f;        // smoothing
    public float dragDelay = 0.2f;     // time before rotation starts

    private Vector2 rotation = Vector2.zero;
    private bool isDragging = false;
    private float mouseDownTime = 0f;

    private Quaternion initialRotation;

    void Start()
    {
        // Save the starting orientation
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownTime = Time.time;
            isDragging = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (!isDragging && Time.time - mouseDownTime >= dragDelay)
                isDragging = true;

            if (isDragging)
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                rotation.x += mouseX * sensitivity;
                rotation.y -= mouseY * sensitivity;
                rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);

                Quaternion targetRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    public void Recenter()
    {
        // Reset camera to initial rotation
        transform.rotation = initialRotation;

        // Reset stored rotation values to match
        Vector3 euler = initialRotation.eulerAngles;
        rotation = new Vector2(euler.y, euler.x);
        // note: swap X/Y because Vector2 stores (yaw, pitch)
    }
}

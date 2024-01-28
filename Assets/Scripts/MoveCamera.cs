using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    
    [Range(50, 500)]
    public float sens;
    public bool canRotate;
    public Transform body;
    public static bool CanLook = true;
    float xRot = 0f;
    private void Update()
    {
        transform.position = body.position;
        if (CanLook)
        {
            float rotX = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
            float rotY = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

            xRot -= rotY;
            xRot = Mathf.Clamp(xRot, -80f, 80f);
            body.Rotate(Vector3.up * rotX);
            if (canRotate)
            {
                transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            }

        }
    }
}
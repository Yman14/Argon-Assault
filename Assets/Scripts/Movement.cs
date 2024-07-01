using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 40f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 8f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -15f;

    float xControl, yControl;

    void OnEnable() {
        movement.Enable();
    }
    void OnDisable() {
        movement.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    void ProcessTranslation()
    {
        xControl = movement.ReadValue<Vector2>().x;
        yControl = movement.ReadValue<Vector2>().y;


        float xOffset = xControl * Time.deltaTime * controlSpeed;
        float yOffset = yControl * Time.deltaTime * controlSpeed;
        float xRawPos = transform.localPosition.x + xOffset;
        float yRawPos = transform.localPosition.y + yOffset;
        float xclampedPos = Mathf.Clamp(xRawPos, -xRange, xRange);
        float yclampedPos = Mathf.Clamp(yRawPos, -yRange, yRange);

        transform.localPosition = new Vector3(xclampedPos, yclampedPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {

        float pitch = transform.localPosition.y * positionPitchFactor + yControl * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xControl * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}

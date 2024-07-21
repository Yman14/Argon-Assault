using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Movement Key")] [SerializeField] InputAction movement;
    [Tooltip("Shoot Key")] [SerializeField] InputAction fire;
    [Tooltip("How fast the the ship move with movement key")]
    [SerializeField] float controlSpeed = 40f;
    [Tooltip("Range of Ship movement relative to the screen")]
    [SerializeField] float xRange = 10f;
    [Tooltip("Range of Ship movement relative to the screen")]
    [SerializeField] float yRange = 8f;
    [Tooltip("Array of lasers")]
    [SerializeField] GameObject[] lasers;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -15f;

    float xControl, yControl;

    void OnEnable() {
        movement.Enable();
        fire.Enable();
    }
    void OnDisable() {
        movement.Disable();
        fire.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

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

    void ProcessFiring()
    {
        //if left-clicked mouse button is pressed then shoot
        if(fire.ReadValue<float>() > 0.5)
        {
            SetLaserActive(true);
        }
        else{
            SetLaserActive(false);
        }
    }

    void SetLaserActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}

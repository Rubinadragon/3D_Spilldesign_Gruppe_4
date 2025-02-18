using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float rotationalSpeed = 10f;
    [SerializeField] private float bottomClamp = -40f;
    [SerializeField] private float topClamp = 70f;

    private float cinemachineTargetPitch;
    private float cinemachineTargetYaw;
    [SerializeField] private int mouseButtonToUse = 0; 


    private void LateUpdate()
    {
        CameraLogic();
    }

    private void CameraLogic()
    {
        // Sjekk om en museknapp er trykket (venstre, høyre eller midt)
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            float mouseX = GetMouseInput("Mouse X");
            float mouseY = GetMouseInput("Mouse Y");

            cinemachineTargetPitch = UpdateRotation(cinemachineTargetPitch, mouseY, bottomClamp, topClamp, true);
            cinemachineTargetYaw = UpdateRotation(cinemachineTargetYaw, mouseX, float.MinValue, float.MaxValue, false);

            ApplyRotations(cinemachineTargetPitch, cinemachineTargetYaw);
        }
    }

    private void ApplyRotations (float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isAxis)
    {
        currentRotation += isAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }

    private float GetMouseInput(string axis)
    {
        return Input.GetAxis(axis) * rotationalSpeed * Time.deltaTime;
    }
}

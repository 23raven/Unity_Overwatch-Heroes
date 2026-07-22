using UnityEngine;

public class ViewModelMotion : MonoBehaviour
{
    [Header("Sway")]
    [SerializeField] private float rotationMultiplier = 2f;
    [SerializeField] private float positionMultiplier = 0.0025f;

    [SerializeField] private float maxRotation = 4f;
    [SerializeField] private float maxPosition = 0.03f;

    [SerializeField] private float swaySmoothness = 12f;

    [Header("Recoil")]
    [SerializeField] private float kickBack = 0.05f;
    [SerializeField] private float kickUp = 5f;
    [SerializeField] private float kickSide = 1.5f;

    [SerializeField] private float recoilReturnSpeed = 8f;
    [SerializeField] private float recoilSnappiness = 18f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector2 lookDelta;

    private Vector3 recoilPosition;
    private Vector3 recoilPositionCurrent;

    private Vector3 recoilRotation;
    private Vector3 recoilRotationCurrent;

    private void Awake()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    public void SetLookInput(Vector2 delta)
    {
        lookDelta = delta;
    }

    public void PlayRecoil()
    {
        recoilPosition += Vector3.back * kickBack;

        recoilRotation += new Vector3(
            kickUp,
            Random.Range(-kickSide, kickSide),
            Random.Range(-kickSide * 0.5f, kickSide * 0.5f));
    }

    private void LateUpdate()
    {
        UpdateRecoil();
        UpdateViewModel();
    }

    private void UpdateViewModel()
    {
        float yaw = Mathf.Clamp(
            -lookDelta.x * rotationMultiplier,
            -maxRotation,
            maxRotation);

        float pitch = Mathf.Clamp(
            lookDelta.y * rotationMultiplier,
            -maxRotation,
            maxRotation);

        Quaternion swayRotation =
            Quaternion.Euler(pitch, yaw, yaw * 0.5f);

        Vector3 swayPosition = new Vector3(
            Mathf.Clamp(-lookDelta.x * positionMultiplier, -maxPosition, maxPosition),
            Mathf.Clamp(-lookDelta.y * positionMultiplier, -maxPosition, maxPosition),
            0f);

        Quaternion finalRotation =
            initialRotation *
            swayRotation *
            Quaternion.Euler(recoilRotationCurrent);

        Vector3 finalPosition =
            initialPosition +
            swayPosition +
            recoilPositionCurrent;

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            finalRotation,
            swaySmoothness * Time.deltaTime);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            finalPosition,
            swaySmoothness * Time.deltaTime);
    }

    private void UpdateRecoil()
    {
        recoilPosition = Vector3.Lerp(
            recoilPosition,
            Vector3.zero,
            recoilReturnSpeed * Time.deltaTime);

        recoilPositionCurrent = Vector3.Lerp(
            recoilPositionCurrent,
            recoilPosition,
            recoilSnappiness * Time.deltaTime);

        recoilRotation = Vector3.Lerp(
            recoilRotation,
            Vector3.zero,
            recoilReturnSpeed * Time.deltaTime);

        recoilRotationCurrent = Vector3.Lerp(
            recoilRotationCurrent,
            recoilRotation,
            recoilSnappiness * Time.deltaTime);
    }
}
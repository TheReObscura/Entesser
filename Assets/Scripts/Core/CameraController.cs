using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Cinemachine")]
    [SerializeField] private CinemachineCamera vcam;
    private CinemachinePositionComposer composer;

    [Header("Deadzone & Idle")]
    [SerializeField] private float idleToCenter = 3f;
    [SerializeField] private float idleReturnSpeed = 2f;
    private float idleTimer = 0f;
    private Vector2 inputVector;
    void Start()
    {
        if (vcam != null)
            composer = vcam.GetComponent<CinemachinePositionComposer>();
        else
            Debug.LogError("CinemachineCamera не присвоена!");
    }

    void Update()
    {
        inputVector = GameInput.instance.GetMovementVector();
        if (composer == null) return;

        if (inputVector != Vector2.zero)
            idleTimer = 0f;
        else
            idleTimer += Time.deltaTime;

        HandleIdleCentering();
    }

    private void HandleIdleCentering()
    {
        if (idleTimer > idleToCenter)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, vcam.transform.position.z);
            Vector3 newPos = Vector3.Lerp(vcam.transform.position, targetPos, Time.deltaTime * idleReturnSpeed);
            vcam.ForceCameraPosition(newPos, vcam.transform.rotation);
        }
    }
}

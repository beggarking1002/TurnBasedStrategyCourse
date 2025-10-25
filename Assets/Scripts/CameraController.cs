using Unity.Cinemachine;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;

    [SerializeField] private CinemachineCamera cinemachineCamera;

    private CinemachineFollow follow;
    private Vector3 targetFollowOffset;

    private void Start()
    {
        //cinemachineTransposer = cinemachineCamera.GetCinemachineComponent<CinemachineTransposer>();
        //targetFollowOffset = cinemachineTransposer.m_FollowOffset;
        follow = cinemachineCamera.GetComponent<CinemachineFollow>();
        if (follow == null)
            follow = cinemachineCamera.gameObject.AddComponent<CinemachineFollow>();
        follow = cinemachineCamera.GetComponent<CinemachineFollow>();
        //follow.BindingMode = CinemachineFollow.BindingMode.WorldSpace;
        if (follow.FollowOffset == Vector3.zero)
            follow.FollowOffset = new Vector3(-8f, 10f, -8f);
        
        targetFollowOffset = follow.FollowOffset;
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        follow.FollowOffset = targetFollowOffset;
    }
    
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }
    
    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }

        float moveSpeed = 10f;

        Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 right   = new Vector3(transform.right.x, 0f, transform.right.z).normalized;

        Vector3 moveVector = forward * inputMoveDir.z + right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
    
    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
    
    private void HandleZoom()
    {
        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        float zoomSpeed = 5f;
        follow.FollowOffset = Vector3.Lerp(follow.FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }


}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
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

}

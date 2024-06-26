using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private float orgJumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public LayerMask jumpAreaMask;
    public bool isMoving = false;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    public GameObject maincamera;
    public GameObject subcamera;

    private Vector2 mouseDelta;

    [HideInInspector]
    //public bool canLook = true;

    private Rigidbody _rigidbody;
    public Vector3 newGravity = new Vector3(0, -10f, 0);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        orgJumpPower = jumpPower;
    }

    void Start()
    {
        //커서 보이지 않게 해줌
        Cursor.lockState = CursorLockMode.Locked;

        Physics.gravity = newGravity;
    }

    void FixedUpdate() //물리연산
    {
        if(curMovementInput == Vector2.zero)
        {
            isMoving = false;
        }
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed; //움직이는 힘을 곱해줌
        dir.y = _rigidbody.velocity.y; // 점프할 때만 y값이 변함
        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnSwitchCamera(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {
            if (maincamera.activeSelf)
            {
                Debug.Log($"{subcamera} active");
                maincamera.SetActive(false);
                subcamera.SetActive(true);
            }
            else
            {
                Debug.Log($"{maincamera} active");
                subcamera.SetActive(false);
                maincamera.SetActive(true);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       if( context.phase == InputActionPhase.Performed) // 키가 눌릴 때 Started, 키가 눌린 후에도 계속 호출하려면 Performed
        {
            isMoving = true;
            curMovementInput = context.ReadValue<Vector2>();
        }
       else if(context.phase == InputActionPhase.Canceled) // 액션 취소됐을 때
        {
            curMovementInput = Vector2.zero;
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            jumpPower = CheckSuperJump();

            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                jumpPower = orgJumpPower;
            }

        }
    }

    // 플레이어가 점프대 위에 있는지 확인
    private float CheckSuperJump()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.2f, jumpAreaMask))
            {
                Debug.Log("Super Jump!");
                return jumpPower * 3.0f;
            }
        }
        return jumpPower;
    }

    // 플레이어가 바닥에 붙어있는지 확인 
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i<rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}

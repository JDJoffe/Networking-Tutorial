using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class CameraLook : NetworkBehaviour
{
    [Header("Variables")]
    //mouse sensitivity
    public float _mouseSensitivity = 20f;
    //min & max Y axis
    public float _minY = 90f;
    public float _maxY = 90f;
    //camera yaw rotation Y
    private float _yaw = 0f;
    //camera pitch rotation x
    private float _pitch = 0f;
    //camera ref
    private GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //lock mouse & invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //ref camera in gameobject
        Camera cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            mainCamera = cam.gameObject;
        }
    }
    //when player dies run this
    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            //update local player cam movement
            HandleInput();
        }
    }
    private void LateUpdate()
    {
        if (isLocalPlayer)
        {
            mainCamera.transform.localEulerAngles = new Vector3(-_pitch, 0, 0);
        }
    }
    void HandleInput()
    {
        float mouseX;
        float mouseY;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        _yaw +=  mouseX * _mouseSensitivity * Time.deltaTime;
        _pitch -= mouseY * _mouseSensitivity * Time.deltaTime;
    
        Mathf.Clamp(_pitch, _minY, _maxY);



    }
}

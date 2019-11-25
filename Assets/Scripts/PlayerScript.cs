using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour
{
    [Header("Variables")]
    public bool _isGrounded = false;
    public float _moveSpeed = 10f, _rotationSpeed = 5f, _jumpSpeed = 2f;
    private Rigidbody _rigid;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();

        AudioListener _audioListener = GetComponentInChildren<AudioListener>();
        Camera _camera = GetComponentInChildren<Camera>();

        //if player is not local disable camera+sounds
        if (isLocalPlayer)
        {
            _camera.enabled = true;
            _audioListener.enabled = true;
        }
        else
        {
            _camera.enabled = false;
            _audioListener.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //can only move localplayer
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }

    public void Move(KeyCode _key)
    {
        Vector3 _pos = _rigid.position;
        Quaternion _rotation = _rigid.rotation;

        //Movement direction
        switch (_key)
        {
            case KeyCode.W:
                _pos += transform.forward * _moveSpeed * Time.deltaTime;
                break;
            case KeyCode.A:
                _rotation *= Quaternion.AngleAxis(-_rotationSpeed, Vector3.up);
                break;
            case KeyCode.S:
                _pos -= transform.forward * _moveSpeed * Time.deltaTime;
                break;
            case KeyCode.D:
                _rotation *= Quaternion.AngleAxis(_rotationSpeed, Vector3.up);
                break;
            case KeyCode.Space:
                if (_isGrounded)
                {
                    _rigid.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
                    _isGrounded = false;
                }
                break;
        }
        //IMPORTANT
        //actually move
        _rigid.MovePosition(_pos);
        _rigid.MoveRotation(_rotation);
    }
    
    void OnCollisionEnter(Collision _col)
    {
        _isGrounded = true;
    }
    void HandleInput()
    {
        //GetKeys
        KeyCode[] keys =
        {
            KeyCode.W,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D,
            KeyCode.Space
        };
        foreach (var _key in keys)
        {
            if (Input.GetKey(_key))
            {
                Move(_key);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncTransform : NetworkBehaviour
{
    [Header("Variables")]
    //speed of lerping rotation&position
    public float _lerpRate = 15;

    //command send threshhold
    public float _positionThreshold = 0.5f;
    public float _rotationThreshold = 5.0f;
    //record prev rotation and position sent to the server
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    //vars synced across network
    [SyncVar] private Vector3 _syncPosition;
    [SyncVar] private Quaternion _syncRotation;

    //get rigidbody 
    private Rigidbody _rigid;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();

        TransmitRotation();
        LerpRotation();
    }
    #region Lerps
    void LerpPosition()
    {
        //if not the local player
        if (!isLocalPlayer)
        {
            //Lerp position of other connected clients
            _rigid.position = Vector3.Lerp(_rigid.position, _syncPosition, Time.deltaTime * _lerpRate);
        }
    }
    void LerpRotation()
    {
        //if not local player
        if (!isLocalPlayer)
        {
            //lerp rotation of other connected clients
            _rigid.rotation = Quaternion.Lerp(_rigid.rotation, _syncRotation, Time.deltaTime * _lerpRate);
        }
    }
    #endregion
    #region Serverstuff
    [Command]
    void CmdSendPositionToServer(Vector3 _position)
    {
        _syncPosition = _position;
        Debug.Log("Position Command");
    }
    [Command]
    void CmdSendRotationToServer(Quaternion _rotation)
    {
        _syncRotation = _rotation;
        Debug.Log("Rotation Command");
    }
    [ClientCallback]
    void TransmitPosition()
    {
        if (isLocalPlayer)
        {
            CmdSendPositionToServer(_rigid.position);
        }
    }
    [ClientCallback]
    void TransmitRotation()
    {
        if (isLocalPlayer)
        {
            CmdSendRotationToServer(_rigid.rotation);
        }
    }
    #endregion
}

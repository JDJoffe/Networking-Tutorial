using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// xml save data
using System.Xml;
using System.Xml.Serialization;
[XmlRoot("Game Data Collection")]
public class GameData
{
    // store data of position and rotation
    public Vector3 _position;
    public Quaternion _rotation;
    [XmlArray("Dialogue")]
    [XmlArrayItem("Text")]
    public string[] dialogue;
}

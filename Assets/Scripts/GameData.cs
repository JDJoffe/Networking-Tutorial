using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
[XmlRoot("Game Data Collection")]
public class GameData
{
    public Vector3 _position;
    public Quaternion _rotation;
    [XmlArray("Dialogue")]
    [XmlArrayItem("Text")]
    public string[] dialogue;
}

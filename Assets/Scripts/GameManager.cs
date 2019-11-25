using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Transform _player;
    // ref to gamedata
    private GameData data = new GameData();
    public string fileName = "GameData.xml";

// load the data stored in the xml file
    public void Load(string path)
    {
        var serializer = new XmlSerializer(typeof(GameData));
        var stream = new FileStream(path, FileMode.Open);
        data = serializer.Deserialize(stream) as GameData;
        stream.Close();
    }
    // write data to the xml file
    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof( GameData));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
        Debug.Log("File Saved Successfully to " + path);
    }



    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath +"/"+ fileName;
        Load(path);
      
    }

    // Update is called once per frame
    void Update()
    {
        // check inputs and save when you press O
        if (Input.GetKeyDown(KeyCode.O))
        {
            _player = FindObjectOfType<PlayerScript>().transform;
            data._position = _player.position;
            data._rotation = _player.rotation;
            data.dialogue = new string[]
            {
                "Hello",
                "World"
            };
            Save(Application.dataPath + "/" + fileName);
        }
        // load data when you hit R
        if (Input.GetKeyDown(KeyCode.R))
        {
            Load(Application.dataPath + "/" + fileName);
            _player = FindObjectOfType<PlayerScript>().transform;
            _player.position = data._position;
            _player.rotation = data._rotation;

        }
    }

}

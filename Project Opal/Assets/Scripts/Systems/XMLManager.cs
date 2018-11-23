using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {

    public static XMLManager singleton;

    void Awake()
    {
        singleton = this;
    }

    public MissionDatabase missionDB;

    public void SaveMissions()
    {
        List<Mission> missions = MissionList.singleton.missionList;

        XmlSerializer serializer = new XmlSerializer(typeof(MissionDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/mission_data.xml", FileMode.Create);

        missionDB = new MissionDatabase();

        for (int i = 0; i < missions.Count; i++)
        {
            missionDB.list.Add(missions[i]);
        }

        serializer.Serialize(stream, missionDB);
        stream.Close();
    }

    public void LoadMissions()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(MissionDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/mission_data.xml", FileMode.Open);

        missionDB = serializer.Deserialize(stream) as MissionDatabase;

        MissionList.singleton.missionList = new List<Mission>();

        for (int i = 0; i < missionDB.list.Count; i++)
        {
            MissionList.singleton.missionList.Add(missionDB.list[i]);
        }

        stream.Close();
    }
}

[System.Serializable]
public class MissionDatabase {

    public List<Mission> list = new List<Mission>();

}

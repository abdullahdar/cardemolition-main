﻿using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class PersistableSO : MonoBehaviour
{

    [Header("Meta")]
    public string persisterName;
    [Header("Scriptable Objects")]
    public List<ScriptableObject> objectsToPersist;

    bool persist = false;

    protected void Awake()
    {       
        for (int i = 0; i < objectsToPersist.Count; i++)
            {
                if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i)))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i), FileMode.Open);                    

                    JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectsToPersist[i]);

                    file.Close();
                persist = true;
                }
                else
                {
                    //Do Nothing
                }
            }       
    } 

    protected void OnDisable()
    {
            for (int i = 0; i < objectsToPersist.Count; i++)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i));
                var json = JsonUtility.ToJson(objectsToPersist[i]);

                /*dev-junaid-v2.5.0b41 */

                bf.Serialize(file, json);


                file.Close();
            }
    }    

    public void SaveData()
    {
        for (int i = 0; i < objectsToPersist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i));
            var json = JsonUtility.ToJson(objectsToPersist[i]);

            /*dev-junaid-v2.5.0b41 */

            bf.Serialize(file, json);


            file.Close();
        }
    }

}

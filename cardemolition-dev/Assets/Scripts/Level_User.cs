using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level_User : ScriptableObject
{
    [System.Serializable]
    public class Levels
    {
        public string levelNo;
        public bool isLocked = true;
        public bool isCompleted = false;


    }
    public List<Levels> levels;
}

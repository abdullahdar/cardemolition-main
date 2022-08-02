using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelsData : ScriptableObject
{
    [System.Serializable]
    public class Levels
    {
        public string levelIndex;
        public string sceneName;
        public int levelNumber;
        public int reward = 1500;

        [SerializeField]
        string levelDisplayName;
        public string LevelDisplayName
        {
            get
            {
                return levelDisplayName;
            }
        }

        public enum Environment
        {
            footBallGround1,
            footBallGround2,
            footBallGround3
        }
        public Environment environment;

        public bool isLocked = true;
        public bool isCompleted = false;
        public bool canRateUs = false;
        public bool hasAds = false;

        [Header("LEVEL REQUIREMENTS")]
        public int requiredCar;
        public int requiredGun;

        [Header("PLAYER CAR")]
        public Vector3 startPosition;
        public Vector3 startRotation;
        public float playerRange = 300f;
        public float playerDamage = 2f;

        [System.Serializable]
        public class EnemyCars
        {
            public string carNo;
            
            public CarType.Car_Type carType;

            [Header("GENERAL SETTINGS")]           
            public int chaseRange;
            public Vector3 spawnPoint;
            public bool fightOtherEnemy = true;
            public bool followPlayer;

            [Header("BARRIER ATTRIBUTES")]
            public bool hasBarrier;
            [Tooltip("Barrier value between 0 and 4.")]
            public int barrierNumber;
            public int barrierDamage;

            [Header("GUN ATTRIBUTES")]
            public bool hasGun;
            [Tooltip("Gun value between 0 and 4.")]
            public int gunNumber;
            public float gunDamage;
            public float fireRate;
            public float gunRange;

            [Header("MISSILE ATTRIBUTES")]
            public bool hasMissile;
            [Tooltip("Missile value between 0 and 4.")]
            public int missileNumber;
            public int missileDamage;
        }
        [Header("ENEMY CARS")]
        public List<EnemyCars> enemyCars;    
    }
    public List<Levels> levels;

    public int levelSelected;

    public int coins;

    public bool openCarSelection = false;

    [Header("Sound Settings")]
    public bool soundOn = true;
    public bool musicOn = true;

    #region Enemy Cars

    public int Total_Enemy(int levelNumber)
    {
        return levels[levelNumber].enemyCars.Count;        
    }
    public CarType.Car_Type EnemyType(int levelNumber,int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].carType;
    }
    public bool HasBarrier(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].hasBarrier;
    }
    public bool HasGun(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].hasGun;
    }
    public bool HasMissile(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].hasMissile;
    }
    public int ChaseRange(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].chaseRange;
    }
    public bool FightOtherEnemy(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].fightOtherEnemy;
    }    
    public bool FollowPlayer(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].followPlayer;
    }
    public int BarrierDamage(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].barrierDamage;
    }
    public float GunDamage(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].gunDamage;
    }
    public float FireRate(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].fireRate;
    }
    public float GunRange(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].gunRange;
    }
    public float MissileDamage(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].missileDamage;
    }
    public Vector3 EnemySpawnPoint(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].spawnPoint;
    }    
    public int TotalEnemyCars(int levelNumber)
    {
        return levels[levelNumber].enemyCars.Count;
    }
    public int BarrierNumber(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].barrierNumber;
    }
    public int GunNumber(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].gunNumber;
    }
    public int MissileNumber(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].missileNumber;
    }
    public string GetEnemyTag(int levelNumber, int enemyNumber)
    {
        string tag = "Enemy";
        if (levels[levelNumber].enemyCars[enemyNumber].fightOtherEnemy)
            tag = "Enemy";
        else
            tag = "Malang";
        return tag;
    }

    #endregion

    #region Player Car
    public Vector3 PlayerPosition(int levelNumber)
    {
        return levels[levelNumber].startPosition;
    }
    public Vector3 PlayerRotation(int levelNumber)
    {
        return levels[levelNumber].startRotation;
    }
    public float PlayerDamage(int levelNumber)
    {
        return levels[levelNumber].playerDamage;
    }

    #endregion

    #region Coins
    public void MinusCoins(int _coins)
    {
        coins -= _coins;
    }
    public void AddCoins(int _coins)
    {
        coins += _coins;
    }

    #endregion

    #region Level Requirement Events

    public int GetRequiredCar(int levelNumber)
    {
        return levels[levelNumber].requiredCar - 1;
    }

    public int GetRequiredGun(int levelNumber)
    {
        return levels[levelNumber].requiredGun - 1;
    }

    #endregion

    #region Level Information
    public int GetReward()
    {
        return levels[levelSelected].reward;
    }
    public int GetSceneBuildIndex(int levelNumber)
    {
        int buildIndex = 0;
        if (levels[levelNumber].environment == Levels.Environment.footBallGround1)
            buildIndex = 1;
        else if (levels[levelNumber].environment == Levels.Environment.footBallGround2)
            buildIndex = 2;
        else if (levels[levelNumber].environment == Levels.Environment.footBallGround3)
            buildIndex = 3;
        return buildIndex;
    }
    public int GetCurrentSceneIndex()
    {
        int buildIndex = 0;
        if (levels[levelSelected].environment == Levels.Environment.footBallGround1)
            buildIndex = 1;
        else if (levels[levelSelected].environment == Levels.Environment.footBallGround2)
            buildIndex = 2;
        else if (levels[levelSelected].environment == Levels.Environment.footBallGround3)
            buildIndex = 3;
        return buildIndex;
    }
    public bool IsLevelLocked(int levelNumber)
    {
        return levels[levelNumber].isLocked;
    }
    public bool IsLevelCompleted(int levelNumber)
    {
        return levels[levelNumber].isCompleted;
    }
    public int TotalEnemies(int levelNumber)
    {
        return levels[levelNumber].enemyCars.Count;
    }
    public int GetLastOpenedLevel()
    {
        int lastOpenedeLevel = -1;

        for(int i = 0; i < levels.Count; i++)
        {
            if (!levels[i].isLocked)
                lastOpenedeLevel++;
            else
                break;
        }

        return lastOpenedeLevel;
    }

    #endregion

    #region Set Level Values

    public void Unlock_NextLevel()
    {
        Give_Reward();

        levels[levelSelected].isCompleted = true;

        if (levelSelected < 17)
        {            
            levels[levelSelected + 1].isLocked = false;
        }        
    }
    public void Give_Reward()
    {
        int completedLevel = -1;
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].isCompleted)
                completedLevel++;
        }

        if (levelSelected > completedLevel)
            AddCoins(GetReward());
    }

    #endregion
}

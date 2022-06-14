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

        public bool isLocked = true;
        public bool isCompleted = false;

        [Header("PLAYER CAR")]
        public Vector3 startPosition;
        public Vector3 startRotation;

        [System.Serializable]
        public class EnemyCars
        {
            public string carNo;
            
            public CarType.Car_Type carType;

            [Header("GENERAL SETTINGS")]           
            public int chaseRange;
            public Vector3 spawnPoint;

            [Header("BARRIER ATTRIBUTES")]
            public bool hasBarrier;
            [Tooltip("Barrier value between 0 and 4.")]
            public int barrierNumber;
            public int barrierDamage;

            [Header("GUN ATTRIBUTES")]
            public bool hasGun;
            [Tooltip("Gun value between 0 and 4.")]
            public int gunNumber;
            public int gunDamage;
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
    public int BarrierDamage(int levelNumber, int enemyNumber)
    {
        return levels[levelNumber].enemyCars[enemyNumber].barrierDamage;
    }
    public int GunDamage(int levelNumber, int enemyNumber)
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
}

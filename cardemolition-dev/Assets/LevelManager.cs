using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("LEVELS ATTRIBUTES")]
    public int levelSelected;
    public GameObject[] levels;
    private int selectedCar;

    [Header("REFERENCES")]
    [SerializeField]
    private bl_IndicatorManager _bl_IndicatorManager;
    private GameManager gameManager;

    [Header("PLAYER ATTRIBUTES")]    
    public GameObject[] playerCar;
    public CinemachineFreeLook playerCamera;
    private Transform playerTransform;

    [Header("ENEMY ATTRIBUTES")]
    public GameObject[] enemyCarUi;
    public GameObject[] enemyCarPrefabs;
    public RCC_AIWaypointsContainer enemyWayPointContainer;

    [Header("DATA CLASSES")]
    public LevelsData levelsData;
    public CarData carData;

    [Header("GAME STATS")]
    private int totalEnemies;
    private int remainingEnemies;
    

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        levelSelected = levelsData.levelSelected;
        OpenLevel(levelSelected);
        totalEnemies = levelsData.TotalEnemies(levelSelected);
        remainingEnemies = totalEnemies;

        SpawnPlayerCar();
        SpawanEnemyCar();
    }
    void SpawnPlayerCar()
    {
        selectedCar = carData.Selected_Car();

        for(int i = 0; i < playerCar.Length; i++)
        {
            if (playerCar[i].GetComponent<CarType>().carType == carData.Car_Type(selectedCar))
            {
                CarType _playerCar = playerCar[i].GetComponent<CarType>();                

                _playerCar.Set_Position(levelsData.PlayerPosition(levelSelected));
                _playerCar.Set_Rotation(levelsData.PlayerRotation(levelSelected));
                
                _playerCar.ActivateBarrier(carData.Selected_Barrier(selectedCar));
                _playerCar.ActivateGun(carData.Selected_Gun(selectedCar));

                _playerCar.ActivatePlayer();

                Shooting_Control shooting_Control = playerCar[i].GetComponent<Shooting_Control>();
                shooting_Control.SetRange(levelsData.levels[levelSelected].playerRange);
                shooting_Control.SetDamage(levelsData.PlayerDamage(levelSelected));

                _bl_IndicatorManager.SetPlayer(_playerCar.transform);

                CameraSettings(playerCar[i].transform);
                playerTransform = playerCar[i].transform;
                break;
            }
        }      
    }
    void CameraSettings(Transform transform)
    {
        playerCamera.Follow = transform;
        playerCamera.LookAt = transform;

        for (int i = 0; i < playerCamera.m_Orbits.Length; i++)
        {
            playerCamera.m_Orbits[i].m_Height = carData.CameraHeight(selectedCar);
            playerCamera.m_Orbits[i].m_Radius = carData.CameraRadius(selectedCar);
        }
        playerCamera.enabled = true;
        playerCamera.m_XAxis.Value = levelsData.PlayerRotation(levelSelected).y;
    }
    void SpawanEnemyCar()
    {
        for(int i = 0; i < levelsData.TotalEnemyCars(levelSelected); i++)
        {
            Spawn_CarUi(i);

            SetEnemyCar(levelsData.EnemyType(levelSelected, i)).GetComponent<RCC_AICarController>().justFollowPlayer = levelsData.FollowPlayer(levelSelected, i);            
            
            GameObject enemyCar = Instantiate(SetEnemyCar(levelsData.EnemyType(levelSelected,i)), levelsData.EnemySpawnPoint(levelSelected,i), Quaternion.identity);
            enemyCar.transform.GetComponent<RCC_AICarController>().canChaseEnemy = levelsData.FightOtherEnemy(levelSelected, i);
            enemyCar.transform.GetComponent<RCC_AICarController>().chaseDistance = levelsData.ChaseRange(levelSelected, i);
            enemyCar.transform.GetComponent<Enemy_Weapon_Controller>().damage = levelsData.GunDamage(levelSelected, i);
            enemyCar.transform.GetComponent<Enemy_Weapon_Controller>().fireRate = levelsData.FireRate(levelSelected, i);
            enemyCar.transform.GetComponent<Enemy_Weapon_Controller>().range = levelsData.GunRange(levelSelected, i);

            if (levelsData.FollowPlayer(levelSelected, i))
            {                
                enemyCar.transform.GetComponent<RCC_AICarController>().targetsInZone.Add(playerTransform);
            }

            enemyCar.transform.tag = levelsData.GetEnemyTag(levelSelected, i);
            enemyCar.GetComponent<RCC_AICarController>().waypointsContainer = enemyWayPointContainer;
            CarType _enemyCar = enemyCar.GetComponent<CarType>();

            _enemyCar.ActivateEnemyBarrier(levelsData.BarrierNumber(levelSelected,i),levelsData.HasBarrier(levelSelected,i));
            _enemyCar.ActivateEnemyGun(levelsData.GunNumber(levelSelected, i), levelsData.HasGun(levelSelected, i));
            enemyCar.transform.GetComponent<DamageManager>().levelManager = this;
        }        
    }
    GameObject SetEnemyCar(CarType.Car_Type _car_Type)
    {
        GameObject _enemyCar = null;
        switch(_car_Type)
        {
            case CarType.Car_Type._4X:
                _enemyCar = enemyCarPrefabs[0];
                break;
            case CarType.Car_Type.aX_car1:
                _enemyCar = enemyCarPrefabs[1];
                break;
            case CarType.Car_Type.b_car_1:
                _enemyCar = enemyCarPrefabs[2];
                break;
            case CarType.Car_Type.buggy:
                _enemyCar = enemyCarPrefabs[3];
                break;
            case CarType.Car_Type.jip:
                _enemyCar = enemyCarPrefabs[4];
                break;
            case CarType.Car_Type.tos:
                _enemyCar = enemyCarPrefabs[5];
                break;
            default:
                break;
        }
        return _enemyCar;
    }

    #region GamePlay Functions
    public void Exclude_Enemy()
    {
        remainingEnemies--;
        Debug.Log("Enemy Number: " + (remainingEnemies - totalEnemies));
        Destroy_CarUi((totalEnemies - remainingEnemies) - 1);

        if (remainingEnemies == 0)
            gameManager.ShowGameComplete();
    }

    public void OpenLevel(int levelNumber)
    {
        levels[levelNumber].SetActive(true);
    }
    #endregion

    #region Enemy Car Ui
    void Spawn_CarUi(int enemyNumber)
    {
        enemyCarUi[enemyNumber].SetActive(true);
    }
    void Destroy_CarUi(int enemyNumber)
    {
        enemyCarUi[enemyNumber].GetComponent<Image>().color = Color.red;
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public int levelSelected;
    private int selectedCar;

    [SerializeField]
    private bl_IndicatorManager _bl_IndicatorManager;
    private GameManager gameManager;

    [Header("PLAYER ATTRIBUTES")]    
    public GameObject[] playerCar;
    public CinemachineFreeLook playerCamera;

    [Header("ENEMY ATTRIBUTES")]
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

                _bl_IndicatorManager.SetPlayer(_playerCar.transform);

                CameraSettings(playerCar[i].transform);
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
    }
    void SpawanEnemyCar()
    {
        for(int i = 0; i < levelsData.TotalEnemyCars(levelSelected); i++)
        {           
            GameObject enemyCar = Instantiate(SetEnemyCar(levelsData.EnemyType(levelSelected,i)), levelsData.EnemySpawnPoint(levelSelected,i), Quaternion.identity);
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

        if (remainingEnemies == 0)
            gameManager.ShowGameComplete();
    }
    #endregion

}

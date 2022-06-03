using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelection : MonoBehaviour
{
    [Header("CAR MENUS")]
    public Canvas carPanel;
    public Canvas barrierPanel;
    public Canvas gunPanel;
    public Canvas missilePanel;
    public Canvas tyresPanel;

    [Header("CAR SELECTION")]
    public int selectedCar = 0;
    public GameObject btnBeginCar;
    public GameObject btnPurchaseCar;
    public Text carPrice;

    [Header("BARRIER SELECTION")]
    public int selectedBarrier = 0;
    public GameObject btnBeginBarrier;
    public GameObject btnPurchaseBarrier;
    public Text barrierPrice;    

    [Header("GUN SELECTION")]
    public int selectedGun = 0;
    public GameObject btnBeginGun;
    public GameObject btnPurchaseGun;
    public Text gunPrice;

    [Header("MISSILE SELECTION")]
    public int selectedMissile = 0;
    public GameObject btnBeginMissile;
    public GameObject btnPurchaseMissile;
    public Text missilePrice;
    public enum SelectedMenu
    {
        carSelection,
        barrierSelection,
        gunSelection,
        missileSelection,
        tyreSelection
    }

    [Header("MENU SELECTION")]
    public SelectedMenu selectedMenu;

    public CarData carData;
    public LevelsData levelsData;
    public Text totalCoins;

    [System.Serializable]
    public class Cars
    {
        public string name;
        public GameObject car;
        public MeshRenderer[] barriers;
        public MeshRenderer[] guns;
        public MeshRenderer[] missiles;
    }
    public List<Cars> cars;

    private void Awake()
    {
        totalCoins.text = levelsData.coins.ToString()+" $";
    }

    void Start()
    {       
        OpenSelectedCar(carData.Selected_Car());
        Open_Car_Menu();        
    }

    #region Car Selection
    public void Previous_Car()
    {
        cars[selectedCar].car.SetActive(false);        
        OpenSelectedCar(carData.Get_Previous_Car(selectedCar));

        Show_Car_Stats();        
    }
    public void Next_Car()
    {
        cars[selectedCar].car.SetActive(false);        
        OpenSelectedCar(carData.Get_Next_Car(selectedCar));

        Show_Car_Stats();        
    }
    void OpenSelectedCar(int selected)
    {
        selectedCar = selected;
        cars[selectedCar].car.SetActive(true);

        OpenSelectedBarrier(carData.Selected_Barrier(selectedCar));
        OpenSelectedGun(carData.Selected_Gun(selectedCar));
        OpenSelectedMissile(carData.Selected_Missile(selectedCar));
    }
    void Show_Car_Stats()
    {
        btnBeginCar.SetActive(!carData.IsLocked(selectedCar));
        btnPurchaseCar.SetActive(carData.IsLocked(selectedCar));
        carPrice.text = carData.GetPrice(selectedCar).ToString()+" $";
        carPrice.enabled = carData.IsLocked(selectedCar);
    }
    public void PurchaseCar()
    {
        if (levelsData.coins >= carData.GetPrice(selectedCar))
        {
            levelsData.MinusCoins(carData.GetPrice(selectedCar));
            totalCoins.text = levelsData.coins.ToString() + " $";

            carData.SetLockedUnlocked(selectedCar);
            carData.Used(selectedCar);

            Show_Car_Stats();
        }
        else
        {
            //menuHandler.alertNotEnoughCoins.SetActive(true);
        }
    }
    public void BeginCar()
    {
        carData.Used(selectedCar);
    }

    #endregion

    #region Barrier Selection
    public void Previous_Barrier()
    {
        cars[selectedCar].barriers[selectedBarrier].enabled = false;
        
        OpenSelectedBarrier(carData.Get_Previous_Barrier(selectedCar, selectedBarrier));

        Show_Barrier_Stats();
    }
    public void Next_Barrier()
    {
        cars[selectedCar].barriers[selectedBarrier].enabled = false;        

        OpenSelectedBarrier(carData.Get_Next_Barrier(selectedCar, selectedBarrier));

        Show_Barrier_Stats();
    }
    void OpenSelectedBarrier(int selected)
    {
        selectedBarrier = selected;
        Debug.Log("selected barrier: "+selectedBarrier);
        cars[selectedCar].barriers[selectedBarrier].enabled = true;
    }
    void Show_Barrier_Stats()
    {                        
        btnBeginBarrier.SetActive(!carData.IsBarrierLocked(selectedCar, selectedBarrier));
        btnPurchaseBarrier.SetActive(carData.IsBarrierLocked(selectedCar, selectedBarrier));
        barrierPrice.text = carData.Get_Barrier_Price(selectedCar, selectedBarrier).ToString() + " $";
        barrierPrice.enabled = carData.IsBarrierLocked(selectedCar,selectedBarrier);
    }
    public void Back_Barrier()
    {
        cars[selectedCar].barriers[selectedBarrier].enabled = false;
        OpenSelectedBarrier(carData.Selected_Barrier(selectedCar));        
    }
    public void PurchaseBarrier()
    {
        if (levelsData.coins >= carData.Get_Barrier_Price(selectedCar, selectedBarrier))
        {
            levelsData.MinusCoins(carData.Get_Barrier_Price(selectedCar, selectedBarrier));
            totalCoins.text = levelsData.coins.ToString() + " $";

            carData.Set_Unlocked_Barrier(selectedCar, selectedBarrier);
            carData.Use_Barrier(selectedCar, selectedBarrier);

            Show_Barrier_Stats();
        }
        else
        {
            //menuHandler.alertNotEnoughCoins.SetActive(true);
        }
    }
    public void BeginBarrier()
    {
        carData.Use_Barrier(selectedCar, selectedBarrier);
    }

    #endregion

    #region Gun Selection
    public void Previous_Gun()
    {
        cars[selectedCar].guns[selectedGun].enabled = false;

        OpenSelectedGun(carData.Get_Previous_Gun(selectedCar, selectedGun));

        Show_Gun_Stats();
    }
    public void Next_Gun()
    {
        cars[selectedCar].guns[selectedGun].enabled = false;

        OpenSelectedGun(carData.Get_Next_Gun(selectedCar, selectedGun));

        Show_Gun_Stats();
    }
    void OpenSelectedGun(int selected)
    {
        selectedGun = selected;
        cars[selectedCar].guns[selectedGun].enabled = true;
    }
    void Show_Gun_Stats()
    {
        btnBeginGun.SetActive(!carData.IsGunLocked(selectedCar, selectedGun));
        btnPurchaseGun.SetActive(carData.IsGunLocked(selectedCar, selectedGun));
        gunPrice.text = carData.Get_Gun_Price(selectedCar, selectedGun).ToString() + " $";
        gunPrice.enabled = carData.IsGunLocked(selectedCar, selectedGun);
    }
    public void Back_Gun()
    {
        cars[selectedCar].guns[selectedGun].enabled = false;
        OpenSelectedGun(carData.Selected_Gun(selectedCar));        
    }
    public void PurchaseGun()
    {
        if (levelsData.coins >= carData.Get_Gun_Price(selectedCar, selectedGun))
        {
            levelsData.MinusCoins(carData.Get_Gun_Price(selectedCar, selectedGun));
            totalCoins.text = levelsData.coins.ToString() + " $";

            carData.Set_Unlocked_Gun(selectedCar, selectedGun);
            carData.Use_Gun(selectedCar, selectedGun);

            Show_Gun_Stats();
        }
        else
        {
            //menuHandler.alertNotEnoughCoins.SetActive(true);
        }
    }
    public void BeginGun()
    {
        carData.Use_Gun(selectedCar, selectedGun);
    }

    #endregion

    #region Missile Selection
    public void Previous_Missile()
    {
        cars[selectedCar].missiles[selectedMissile].enabled = false;

        OpenSelectedMissile(carData.Get_Previous_Missile(selectedCar, selectedMissile));

        Show_Missile_Stats();
    }
    public void Next_Missile()
    {
        cars[selectedCar].missiles[selectedMissile].enabled = false;

        OpenSelectedMissile(carData.Get_Next_Missile(selectedCar, selectedMissile));

        Show_Missile_Stats();
    }
    void OpenSelectedMissile(int selected)
    {
        selectedMissile = selected;
        cars[selectedCar].missiles[selectedMissile].enabled = true;
    }
    void Show_Missile_Stats()
    {
        btnBeginMissile.SetActive(!carData.IsMissileLocked(selectedCar, selectedMissile));
        btnPurchaseMissile.SetActive(carData.IsMissileLocked(selectedCar, selectedMissile));
        missilePrice.text = carData.Get_Missile_Price(selectedCar, selectedMissile).ToString() + " $";
        missilePrice.enabled = carData.IsMissileLocked(selectedCar, selectedMissile);
    }
    public void Back_Missile()
    {
        cars[selectedCar].missiles[selectedMissile].enabled = false;
        OpenSelectedMissile(carData.Selected_Missile(selectedCar));        
    }
    public void PurchaseMissile()
    {
        if (levelsData.coins >= carData.Get_Missile_Price(selectedCar, selectedMissile))
        {
            levelsData.MinusCoins(carData.Get_Missile_Price(selectedCar, selectedMissile));
            totalCoins.text = levelsData.coins.ToString() + " $";

            carData.Set_Unlocked_Missile(selectedCar, selectedMissile);
            carData.Use_Missile(selectedCar, selectedMissile);

            Show_Missile_Stats();
        }
        else
        {
            //menuHandler.alertNotEnoughCoins.SetActive(true);
        }
    }
    public void BeginMissile()
    {
        carData.Use_Missile(selectedCar, selectedMissile);
    }

    #endregion

    #region Tyre Selection
    void OpenTyreStatus()
    {

    }

    #endregion

    #region Open Selected Menu
    public void Open_Car_Menu()
    {
        OpenMenu(SelectedMenu.carSelection);
    }
    public void Open_Barrier_Menu()
    {        
        if (carData.IsLocked(selectedCar))
            return;
        OpenMenu(SelectedMenu.barrierSelection);
    }
    public void Open_Gun_Menu()
    {
        if (carData.IsLocked(selectedCar))
            return;
        OpenMenu(SelectedMenu.gunSelection);
    }
    public void Open_Missile_Menu()
    {
        if (carData.IsLocked(selectedCar))
            return;
        OpenMenu(SelectedMenu.missileSelection);
    }
    public void Open_Tyre_Menu()
    {
        if (carData.IsLocked(selectedCar))
            return;
        OpenMenu(SelectedMenu.tyreSelection);
    }
    void OpenMenu(SelectedMenu _selectedMenu)
    {
        Hide_Selected_Menu();
        switch (_selectedMenu)
        {
            case SelectedMenu.carSelection:
                carPanel.enabled = true;
                selectedMenu = _selectedMenu;
                Show_Car_Stats();
                break;
            case SelectedMenu.barrierSelection:
                barrierPanel.enabled = true;
                selectedMenu = _selectedMenu;
                Show_Barrier_Stats();
                break;
            case SelectedMenu.gunSelection:
                gunPanel.enabled = true;
                selectedMenu = _selectedMenu;
                Show_Gun_Stats();
                break;
            case SelectedMenu.missileSelection:
                missilePanel.enabled = true;
                selectedMenu = _selectedMenu;
                Show_Missile_Stats();
                break;
            case SelectedMenu.tyreSelection:
                tyresPanel.enabled = true;
                selectedMenu = _selectedMenu;
                //OpenTyreStatus();
                break;
            default:
                break;
        }
    }
    void Hide_Selected_Menu()
    {
        switch (selectedMenu)
        {
            case SelectedMenu.carSelection:
                carPanel.enabled = false;
                break;
            case SelectedMenu.barrierSelection:
                barrierPanel.enabled = false;
                Back_Barrier();
                break;
            case SelectedMenu.gunSelection:
                gunPanel.enabled = false;
                Back_Gun();
                break;
            case SelectedMenu.missileSelection:
                missilePanel.enabled = false;
                Back_Missile();
                break;
            case SelectedMenu.tyreSelection:
                tyresPanel.enabled = false;
                break;
            default:
                break;
        }
    }
   
    #endregion
}

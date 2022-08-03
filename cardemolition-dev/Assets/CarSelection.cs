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
    public GameObject[] carIconList;
    public Text carName;
    public Text carPrice;

    [Header("BARRIER SELECTION")]
    public int selectedBarrier = 0;
    public GameObject btnBeginBarrier;
    public GameObject btnPurchaseBarrier;
    public GameObject[] barrierIconList;
    public Text barrierPrice;    

    [Header("GUN SELECTION")]
    public int selectedGun = 0;
    public GameObject btnBeginGun;
    public GameObject btnPurchaseGun;
    public GameObject[] gunIconList;
    public Text gunPrice;

    [Header("MISSILE SELECTION")]
    public int selectedMissile = 0;
    public GameObject btnBeginMissile;
    public GameObject btnPurchaseMissile;
    public GameObject[] missileIconList;
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

    [Header("MENU SELECTION SPRITES")]
    public Button btnCarSelection;
    public Button btnBarrierSelection;
    public Button btnGunSelection;
    public Button btnMissileSelection;
    public Image selectedMenuImage;
    public Sprite carSprite;
    public Sprite gunSprite;
    public Sprite barrierSprite;
    public Sprite missileSprite;

    [Header("POP UP THINGS")]
    public Canvas popUp;
    public Image itemPopUpImage;
    public GameObject btnBuy;
    public GameObject btnUse;
    public GameObject btnBegin;
    public GameObject btnWatchVideo;
    public GameObject coinBox;
    public Text alertText;
    public Text itemPrice;
    public Text itemName;

    [Header("LEVEL REQUIREMENT")]
    private int requiredCar;
    private int requiredGun;
    private int requiredCoins;

    [Header("DATA CLASSES")]
    public CarData carData;
    public LevelsData levelsData;
    public CarTextures carTextures;
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

    private MenuManager menuManager;
    private Reward_Video_Handler reward_Video_Handler;

    [SerializeField]
    AudioManager audioManager;

    private void Awake()
    {        
        menuManager = GetComponent<MenuManager>();
        reward_Video_Handler = GetComponent<Reward_Video_Handler>();
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
    public void OnClick_Car(int selected)
    {
        cars[selectedCar].car.SetActive(false);
        OpenSelectedCar(selected);
        Show_Car_Stats();
    }
    void Show_Car_Stats()
    {
        btnBeginCar.SetActive(!carData.IsLocked(selectedCar));
        btnPurchaseCar.SetActive(carData.IsLocked(selectedCar));
        carPrice.text = carData.GetPrice(selectedCar).ToString()+" $";
        carPrice.enabled = carData.IsLocked(selectedCar);
        carName.text = carData.Get_Car_Name(selectedCar).ToUpper();
        CarIconList_Status();
    }
    public void PurchaseCar()
    {
        if (levelsData.coins >= carData.GetPrice(selectedCar))
        {
            levelsData.MinusCoins(carData.GetPrice(selectedCar));            
            menuManager.SetCoins();

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
        audioManager.Play("buttonClickSound");

        carData.Used(selectedCar);        

        if (AllRequirement_Completed())
            menuManager.Show_Loading(levelsData.GetCurrentSceneIndex());
        else
            ShowPopUp();
    }

    bool AllRequirement_Completed()
    {
        bool levelRequirement = false;

        requiredCar = levelsData.GetRequiredCar(levelsData.levelSelected);
        requiredGun = levelsData.GetRequiredGun(levelsData.levelSelected);

        if (selectedCar < requiredCar)
        {
            levelRequirement = false;
            SetPopUp_Car();
        }
        else if (selectedCar == requiredCar && selectedGun < requiredGun)
        {
            SetPopUp_Gun();
            levelRequirement = false;
        }
        else
            levelRequirement = true;       

        return levelRequirement;
    }

    void CarIconList_Status()
    {
        for(int i = 0; i < carIconList.Length; i++)
        {
            if (carData.carData[i].isLocked)
            {
                carIconList[i].transform.GetComponent<Image>().color = Color.gray;
                carIconList[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
            }
            else
            {
                carIconList[i].transform.GetComponent<Image>().color = Color.white;
                carIconList[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
            }
            CarName(i);
        }
    }
    void CarName(int carNumber)
    {
        carIconList[carNumber].transform.GetChild(0).GetComponent<Text>().text = carData.carData[carNumber].carName.ToUpper();
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
        cars[selectedCar].barriers[selectedBarrier].enabled = true;
    }
    public void OnClick_Barrier(int selected)
    {
        cars[selectedCar].barriers[selectedBarrier].enabled = false;
        OpenSelectedBarrier(selected);
        Show_Barrier_Stats();      
    }
    void Show_Barrier_Stats()
    {                        
        btnBeginBarrier.SetActive(!carData.IsBarrierLocked(selectedCar, selectedBarrier));
        btnPurchaseBarrier.SetActive(carData.IsBarrierLocked(selectedCar, selectedBarrier));
        barrierPrice.text = carData.Get_Barrier_Price(selectedCar, selectedBarrier).ToString() + " $";
        barrierPrice.enabled = carData.IsBarrierLocked(selectedCar,selectedBarrier);
        BarrierIconList_Status();
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
            menuManager.SetCoins();

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

        if (AllRequirement_Completed())
            menuManager.Show_Loading(levelsData.GetCurrentSceneIndex());
        else
            ShowPopUp();
    }
    void BarrierIconList_Status()
    {
        for (int i = 0; i < barrierIconList.Length; i++)
        {
            if (carData.IsBarrierLocked(selectedCar,i))
            {
                barrierIconList[i].transform.GetComponent<Image>().color = Color.gray;
                barrierIconList[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
            }
            else
            {
                barrierIconList[i].transform.GetComponent<Image>().color = Color.white;
                barrierIconList[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
            }
        }
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
    public void OnClick_Gun(int selected)
    {
        cars[selectedCar].guns[selectedGun].enabled = false;
        OpenSelectedGun(selected);
        Show_Gun_Stats();
    }
    void Show_Gun_Stats()
    {
        btnBeginGun.SetActive(!carData.IsGunLocked(selectedCar, selectedGun));
        btnPurchaseGun.SetActive(carData.IsGunLocked(selectedCar, selectedGun));
        gunPrice.text = carData.Get_Gun_Price(selectedCar, selectedGun).ToString() + " $";
        gunPrice.enabled = carData.IsGunLocked(selectedCar, selectedGun);
        GunIconList_Status();
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
            menuManager.SetCoins();

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

        if (AllRequirement_Completed())
            menuManager.Show_Loading(levelsData.GetCurrentSceneIndex());
        else
            ShowPopUp();
    }
    void GunIconList_Status()
    {
        for (int i = 0; i < gunIconList.Length; i++)
        {
            if (carData.IsGunLocked(selectedCar, i))
            {
                gunIconList[i].transform.GetComponent<Image>().color = Color.gray;
                gunIconList[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
            }
            else
            {
                gunIconList[i].transform.GetComponent<Image>().color = Color.white;
                gunIconList[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
            }
        }
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
    public void OnClick_Missile(int selected)
    {
        cars[selectedCar].missiles[selectedMissile].enabled = false;

        OpenSelectedMissile(selected);
        Show_Missile_Stats();
    }
    void Show_Missile_Stats()
    {
        btnBeginMissile.SetActive(!carData.IsMissileLocked(selectedCar, selectedMissile));
        btnPurchaseMissile.SetActive(carData.IsMissileLocked(selectedCar, selectedMissile));
        missilePrice.text = carData.Get_Missile_Price(selectedCar, selectedMissile).ToString() + " $";
        missilePrice.enabled = carData.IsMissileLocked(selectedCar, selectedMissile);
        MissileIconList_Status();
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
            menuManager.SetCoins();

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

        if (AllRequirement_Completed())
            menuManager.Show_Loading(levelsData.GetCurrentSceneIndex());
        else
            ShowPopUp();
    }
    void MissileIconList_Status()
    {
        for (int i = 0; i < missileIconList.Length; i++)
        {
            if (carData.IsMissileLocked(selectedCar, i))
            {
                missileIconList[i].transform.GetComponent<Image>().color = Color.gray;
                missileIconList[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
            }
            else
            {
                missileIconList[i].transform.GetComponent<Image>().color = Color.white;
                missileIconList[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
            }
        }
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
                btnCarSelection.interactable = false;
                selectedMenuImage.sprite = carSprite;
                break;
            case SelectedMenu.barrierSelection:
                barrierPanel.enabled = true;
                selectedMenu = _selectedMenu;
                Show_Barrier_Stats();                
                btnBarrierSelection.interactable = false;
                selectedMenuImage.sprite = barrierSprite;
                break;
            case SelectedMenu.gunSelection:
                gunPanel.enabled = true;
                selectedMenu = _selectedMenu;
                Show_Gun_Stats();                
                btnGunSelection.interactable = false;
                selectedMenuImage.sprite = gunSprite;
                break;
            case SelectedMenu.missileSelection:
                missilePanel.enabled = true;
                selectedMenu = _selectedMenu;
                Show_Missile_Stats();                
                btnMissileSelection.interactable = false;
                selectedMenuImage.sprite = missileSprite;
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
                btnCarSelection.interactable = true;
                break;
            case SelectedMenu.barrierSelection:
                barrierPanel.enabled = false;
                Back_Barrier();             
                btnBarrierSelection.interactable = true;
                break;
            case SelectedMenu.gunSelection:
                gunPanel.enabled = false;
                Back_Gun();                
                btnGunSelection.interactable = true;
                break;
            case SelectedMenu.missileSelection:
                missilePanel.enabled = false;
                Back_Missile();                
                btnMissileSelection.interactable = true;
                break;
            case SelectedMenu.tyreSelection:
                tyresPanel.enabled = false;
                break;
            default:
                break;
        }
    }

    #endregion

    #region PopUp

    void ShowPopUp()
    {
        popUp.enabled = true;
        menuManager.cars.SetActive(false);
    }
    void SetPopUp_Car()
    {
        itemPopUpImage.sprite = carTextures.GetCarTexture(requiredCar);
        btnBuy.SetActive(carData.IsLocked(requiredCar));
        btnBuy.GetComponent<Button>().onClick.AddListener(BuyCar);
        btnUse.SetActive(!carData.IsLocked(requiredCar));
        btnUse.GetComponent<Button>().onClick.AddListener(UseCar);
        alertText.text = "You Need To Upgrade Car To Play Next Level !";
        itemPrice.text = carData.GetPrice(requiredCar).ToString();
        itemName.text = carData.Get_Car_Name(requiredCar);
    }
    void SetPopUp_Gun()
    {
        itemPopUpImage.sprite = carTextures.GetGunTexture(selectedCar,requiredGun);
        btnBuy.SetActive(carData.IsGunLocked(selectedCar,requiredGun));
        btnBuy.GetComponent<Button>().onClick.AddListener(BuyGun);
        btnUse.SetActive(!carData.IsGunLocked(selectedCar, requiredGun));
        btnUse.GetComponent<Button>().onClick.AddListener(UseGun);
        alertText.text = "You Need To Upgrade Gun To Play Next Level !";
        itemPrice.text = carData.Get_Gun_Price(selectedCar, requiredGun).ToString();
        itemName.text = carData.Get_Gun_Name(selectedCar, requiredGun);
    }
    public void UseCar()
    {
        carData.Used(requiredCar);        

        alertText.text = "CAR EQUIPPED";
        coinBox.SetActive(false);

        btnUse.SetActive(!carData.isUsed(requiredCar));
        btnBegin.SetActive(carData.isUsed(requiredCar));

        cars[selectedCar].car.SetActive(false);  //<= Deactivate Previous Car
        selectedCar = carData.Selected_Car();

        OpenSelectedCar(selectedCar);
        Show_Car_Stats();       
    }
    public void BuyCar()
    {
        requiredCoins = carData.GetPrice(requiredCar);

        if (levelsData.coins >= requiredCoins)
        {            
            levelsData.MinusCoins(requiredCoins);      
            menuManager.SetCoins();

            carData.SetLockedUnlocked(requiredCar);
            carData.Used(requiredCar);

            btnBegin.SetActive(!carData.IsLocked(requiredCar));
            btnBuy.SetActive(carData.IsLocked(requiredCar));
            coinBox.SetActive(false);

            alertText.text = "PURCHASED SUCCESSFULLY";

            cars[selectedCar].car.SetActive(false);  //<= Deactivate Previous Car
            selectedCar = carData.Selected_Car();

            OpenSelectedCar(selectedCar);
            Show_Car_Stats();
        }
        else
        {
            btnBegin.SetActive(false);
            btnBuy.SetActive(false);
            btnWatchVideo.SetActive(true);
            btnWatchVideo.GetComponent<Button>().onClick.AddListener(WatchVideo_Car);
            alertText.text = "NOT ENOUGH COINS";
        }
    }
    public void UseGun()
    {
        carData.Use_Gun(selectedCar, requiredGun);

        alertText.text = "GUN EQUIPPED";
        coinBox.SetActive(false);

        btnUse.SetActive(!carData.IsUsedGun(selectedCar, requiredGun));
        btnBegin.SetActive(carData.IsUsedGun(selectedCar, requiredGun));

        cars[selectedCar].guns[selectedGun].enabled = false;  //<= Deactivate Previous Gun
        selectedGun = carData.Selected_Gun(selectedCar);

        OpenSelectedGun(selectedGun);
        Show_Gun_Stats();
    }
    public void BuyGun()
    {
        requiredCoins = carData.Get_Gun_Price(selectedCar,requiredGun);

        if (levelsData.coins >= requiredCoins)
        {
            levelsData.MinusCoins(requiredCoins);          
            menuManager.SetCoins();

            carData.Set_Unlocked_Gun(selectedCar,requiredGun);
            carData.Use_Gun(selectedCar, requiredGun);

            btnBegin.SetActive(!carData.IsGunLocked(selectedCar, requiredGun));
            btnBuy.SetActive(carData.IsGunLocked(selectedCar, requiredGun));
            coinBox.SetActive(false);

            alertText.text = "PURCHASED SUCCESSFULLY";

            cars[selectedCar].guns[selectedGun].enabled = false;  //<= Deactivate Previous Gun
            selectedGun = carData.Selected_Gun(selectedCar);

            OpenSelectedGun(selectedGun);
            Show_Gun_Stats();
        }
        else
        {
            btnBegin.SetActive(false);
            btnBuy.SetActive(false);
            btnWatchVideo.SetActive(true);
            btnWatchVideo.GetComponent<Button>().onClick.AddListener(WatchVideo_Gun);
            alertText.text = "NOT ENOUGH COINS";
        }
    }
    public void Skip_PopUp()
    {
        menuManager.Show_Loading(levelsData.GetCurrentSceneIndex());
    }
    public void Disable_PopUp()
    {
        btnBuy.GetComponent<Button>().onClick.RemoveAllListeners();
        btnUse.GetComponent<Button>().onClick.RemoveAllListeners();
        btnWatchVideo.GetComponent<Button>().onClick.RemoveAllListeners();
        popUp.enabled = false;        
    }
    public void WatchVideo_Car()
    {
        reward_Video_Handler.Watch_Video(requiredCoins, Reward_Video_Handler.RewardType.carReward);       
        btnWatchVideo.SetActive(false);
        Disable_PopUp();
    }
    public void WatchVideo_Gun()
    {
        reward_Video_Handler.Watch_Video(requiredCoins, Reward_Video_Handler.RewardType.gunReward);
        Disable_PopUp();
        btnWatchVideo.SetActive(false);
    }
    #endregion
}

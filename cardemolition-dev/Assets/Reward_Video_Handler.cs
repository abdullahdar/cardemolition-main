using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_Video_Handler : MonoBehaviour
{
    [Header("REWARDED PANEL THINGS")]
    public Canvas rewardedPanel;
    public Text rewardTitle;
    public Text rewardDescription;
    public GameObject rewardCoinBar;
    public GameObject btnWatchVideo;
    public GameObject btnBuyGun;
    private int totalcoins;
    private int requiredCoins;    

    public enum RewardType
    {
        carReward,
        gunReward
    }
    public RewardType rewardType;

   // private CutScene_Manager cutScene_Manager;
    private CarSelection carSelection;
    private LevelsData levelsData;
    private MenuManager menuManager;

    private void Awake()
    {        
        carSelection = GetComponent<CarSelection>();
        levelsData = carSelection.levelsData;
        menuManager = GetComponent<MenuManager>();
    }
    public void ShowRewardSuccessfully()
    {
        menuManager.cars.SetActive(false);
        levelsData.AddCoins(50);
        rewardedPanel.enabled = true;
        rewardTitle.color = Color.white;
        rewardTitle.text = "Congratulation !";
        rewardDescription.text = "You Have Been Rewarded By 50 Coins!";
        rewardCoinBar.SetActive(true);        

        totalcoins = levelsData.coins;

        if (totalcoins >= requiredCoins)
        {
            btnBuyGun.SetActive(true);            
            Show_Btn_Rewarded(false);
        }
        else
        {            
            Show_Btn_Rewarded(true);
            btnBuyGun.SetActive(false);
        }
    }
    public void RewardFailedToLoad()
    {
        rewardedPanel.enabled = true;
        rewardTitle.color = Color.red;
        rewardTitle.text = "Video Not Available !";
        rewardDescription.text = "Reward Video Not Available!" + "\n" + "Please Try Again!";
        rewardCoinBar.SetActive(false);
    }
    public void Watch_Video(int _requiredCoins, RewardType _rewardType)
    {
        requiredCoins = _requiredCoins;
        rewardType = _rewardType;
        //AdsManager.Instance.ShowunityadmobRewardVideo(AdsManager.RewardType.Store_Coin_Reward);
                
        ShowRewardSuccessfully();
        //RewardFailedToLoad();
    }
    public void Watch_Video_Again()
    {
        ShowRewardSuccessfully();
    }
    public void Show_Btn_Rewarded(bool status)
    {
        btnWatchVideo.SetActive(status);        
    }
    public void BuyGun()
    {
        rewardedPanel.enabled = false;        

        if (rewardType == RewardType.carReward)
        {
            carSelection.popUp.enabled = true;
            carSelection.BuyCar();            
        }
        else if (rewardType == RewardType.gunReward)
        {
            carSelection.popUp.enabled = true;
            carSelection.BuyGun();            
        }
    }
}

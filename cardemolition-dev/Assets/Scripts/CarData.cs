using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CarData : ScriptableObject
{
    [System.Serializable]
    public class Cars
    {        
        public string carName;
        public string carDescription;
        
        public bool isUsed = false;
        public bool isLocked = false;

        public int price;
       
        public CarType.Car_Type carType;

        [Header("CAMERA SETTINGS")]
        public float cameraHeight;
        public float cameraRadius;

        [System.Serializable]
        public class Barriers : Barrier
        {            
        }
        public List<Barriers> barriers;

        [System.Serializable]
        public class Guns : Gun
        {
        }
        public List<Guns> guns;

        [System.Serializable]
        public class Missiles : Missile
        {
        }
        public List<Missiles> missiles;

        #region Barrier Loacal Functions
        public int Selected_Barrier()
        {
            int selectedBarrier = 0;
            for (int i = 0; i < barriers.Count; i++)
            {
                if (barriers[i].isUsed)
                {
                    selectedBarrier = i;
                    break;
                }
            }
            return selectedBarrier;
        }
        public bool Is_Barrier_Locked(int barrierNumber)
        {            
            return barriers[barrierNumber].isLocked;
        }
        public void Set_Unlocked_Barrier(int barrierNumber)
        {            
            barriers[barrierNumber].isLocked = false;
        }
        public void Unlock_All_Barriers()
        {
            for (int i = 0; i < barriers.Count; i++)
            {
                barriers[i].isLocked = false;
            }            
        }
        public int Get_Barrier_Price(int barrierNo)
        {
            return barriers[barrierNo].price;
        }
        public void Use_Barrier(int a)
        {                        
            for (int i = 0; i < barriers.Count; i++)
            {
                if (i == a)
                {
                    barriers[i].isUsed = true;
                }
                else
                {
                    barriers[i].isUsed = false;
                }
            }
        }
        void UnUsed_Barriers()
        {
            for (int i = 0; i < barriers.Count; i++)
            {
                barriers[i].isUsed = false;
            }          
        }
        bool Is_Barrier_Used(int barrierNumber)
        {            
            return barriers[barrierNumber].isUsed;
        }
        public bool Is_Barrier_Available()
        {
            bool isBarrierAvailable = false;

            for (int i = 0; i < barriers.Count; i++)
            {
                if (barriers[i].isLocked)
                {
                    isBarrierAvailable = true;
                    break;
                }
            }
            return isBarrierAvailable;
        }
        public int Get_Available_Barrier()
        {
            int available_Barrier_No = 0;

            for (int i = 0; i < barriers.Count; i++)
            {
                if (barriers[i].isLocked)
                {
                    available_Barrier_No = i;
                    break;
                }
            }
            return available_Barrier_No;
        }
        #endregion

        #region Guns Local Function

        public int Selected_Gun()
        {
            int selectedGun = 0;
            for (int i = 0; i < guns.Count; i++)
            {
                if (guns[i].isUsed)
                {
                    selectedGun = i;
                    break;
                }
            }
            return selectedGun;
        }
        public bool Is_Gun_Locked(int gunNumber)
        {          
            return guns[gunNumber].isLocked;
        }
        public bool Is_Gun_Used(int gunNumber)
        {
            return guns[gunNumber].isUsed;
        }
        public void Set_Unlocked_Gun(int gunNumber)
        {
            guns[gunNumber].isLocked = false;
        }
        public void Use_Gun(int gunNumber)
        {
            for (int i = 0; i < guns.Count; i++)
            {
                if (i == gunNumber)
                {
                    guns[i].isUsed = true;
                }
                else
                {
                    guns[i].isUsed = false;
                }
            }
        }
        public int Get_Gun_Price(int gunNumber)
        {
            return guns[gunNumber].price;
        }
        public string Get_Gun_Name(int gunNumber)
        {
            return guns[gunNumber].name;
        }

        #endregion

        #region Missiles Local Function
        public int Selected_Missile()
        {
            int selectedMissile = 0;
            for (int i = 0; i < missiles.Count; i++)
            {
                if (missiles[i].isUsed)
                {
                    selectedMissile = i;
                    break;
                }
            }
            return selectedMissile;
        }
        public bool Is_Missile_Locked(int missileNumber)
        {        
            return missiles[missileNumber].isLocked;
        }
        public void Set_Unlocked_Missile(int missileNumber)
        {
            missiles[missileNumber].isLocked = false;
        }
        public void Use_Missile(int missileNumber)
        {
            for (int i = 0; i < missiles.Count; i++)
            {
                if (i == missileNumber)
                {
                    missiles[i].isUsed = true;
                }
                else
                {
                    missiles[i].isUsed = false;
                }
            }
        }
        public int Get_Missile_Price(int missileNo)
        {
            return missiles[missileNo].price;
        }

        #endregion

    }
    public List<Cars> carData;
    public bool reward_Achieved = false;

    #region Car Functions
    public int GetPrice(int carNo)
    {
        return carData[carNo].price;
    }
    public void Used(int carNo)
    {
        for (int i = 0; i < carData.Count; i++)
        {
            if (i == carNo)
            {
                carData[i].isUsed = true;               
            }
            else
            {
                carData[i].isUsed = false;
            }
        }
    }
    public void UnUsed()
    {

        try
        {
            for (int i = 0; i < carData.Count; i++)
            {
                carData[i].isUsed = false;
            }
        }
        catch (System.Exception)
        {


        }

    }
    public bool isUsed(int carNo)
    {        
        return carData[carNo].isUsed;
    }
    public int Selected_Car()
    {
        int selectedCar = 0;

        for (int i = 0; i < carData.Count; i++)
        {
            if (carData[i].isUsed)
            {
                selectedCar = i;
                break;
            }
        }
        return selectedCar;
    }
    public bool IsLocked(int carNo)
    {
        return carData[carNo].isLocked;
    }
    public void SetLockedUnlocked(int carNo)
    {
        carData[carNo].isLocked = false;
    }
    public void UnlockAll()
    {
        try
        {
            for (int i = 0; i < carData.Count; i++)
            {
                carData[i].isLocked = false;
            }
        }
        catch (System.Exception)
        {


        }
    }
    public bool Is_Car_Available()
    {
        bool isCarAvailable = false;

        for (int i = 0; i < carData.Count; i++)
        {
            if (carData[i].isLocked)
            {
                isCarAvailable = true;
                break;
            }
        }

        return isCarAvailable;
    }
    public int Get_Available_Car()
    {
        int get_Available_Car = 0;

        for (int i = 0; i < carData.Count; i++)
        {
            if (carData[i].isLocked)
            {
                get_Available_Car = i;
                break;
            }
        }

        return get_Available_Car;
    }
    public int Get_Next_Car(int selectedCar)
    {
        int nextCar = selectedCar;
        
        nextCar = (nextCar + 1) % carData.Count;
        return nextCar;
    }
    public int Get_Previous_Car(int selectedCar)
    {
        int previousCar = selectedCar;

        if (selectedCar > 0)
        {
            selectedCar--;
            previousCar = selectedCar;
        }
        return previousCar;
    }                    
    public string Get_Car_Name(int carNumber)
    {
        return carData[carNumber].carName;
    }
    public string Get_Car_Description(int carNumber)
    {
        return carData[carNumber].carDescription;
    }    
    public CarType.Car_Type Car_Type(int selectedCar)
    {
        return carData[selectedCar].carType;
    }

    #endregion

    #region Barrier
    public Sprite Get_Skin_Texture(int i)
    {
        Sprite selectedBarrier = null;
        //selectedBarrier = weaponsTexture.weaponSkins[Selected_Car()].barriers[i].texture;

        if (selectedBarrier == null)
            Debug.Log("null");

        return selectedBarrier;
    }    
    public int Selected_Barrier(int selectedCar)
    {      
        return carData[selectedCar].Selected_Barrier();
    }
    public bool IsBarrierLocked(int carNumber, int barrierNumber)
    {
        bool is_Locked = false;

        is_Locked = carData[carNumber].Is_Barrier_Locked(barrierNumber);

        return is_Locked;
    }
    public int Get_Barrier_Price(int barrierNumber)
    {
        int price = 0;

        price = carData[Selected_Car()].Get_Barrier_Price(barrierNumber);
        return price;
    }
    public int Get_Barrier_Price(int carNumber, int barrierNumber)
    {
        int price = 0;

        price = carData[carNumber].Get_Barrier_Price(barrierNumber);
        return price;
    }
    public void Set_Unlocked_Barrier(int barrierNumber)
    {
        carData[Selected_Car()].Set_Unlocked_Barrier(barrierNumber);
    }
    public void Set_Unlocked_Barrier(int carNumber, int barrierNumber)
    {
        carData[carNumber].Set_Unlocked_Barrier(barrierNumber);
    }
    public void Use_Barrier(int barrierNumber)
    {
        carData[Selected_Car()].Use_Barrier(barrierNumber);
    }
    public void Use_Barrier(int carNumber, int barrierNumber)
    {
        carData[carNumber].Use_Barrier(barrierNumber);
    }
    public bool Is_Barrier_Available()
    {
        bool isBarrierAvailable = false;

        isBarrierAvailable = carData[Selected_Car()].Is_Barrier_Available();

        return isBarrierAvailable;
    }
    public int Get_Available_Barrier()
    {
        int availableBarrier = 0;

        availableBarrier = carData[Selected_Car()].Get_Available_Barrier();

        return availableBarrier;
    }
    public void Unlock_All_Barriers()
    {
        try
        {
            for (int i = 0; i < carData.Count; i++)
            {
                carData[i].Unlock_All_Barriers();
            }
        }
        catch (System.Exception)
        {


        }

    }
    public int Get_Next_Barrier(int carNumber, int selectedBarrier)
    {
        int nextSkin = selectedBarrier;

        if (selectedBarrier < carData[carNumber].barriers.Count - 1)
        {
            selectedBarrier++;
            nextSkin = selectedBarrier;
        }
        return nextSkin;
    }
    public int Get_Previous_Barrier(int carNumber, int selectedBarrier)
    {
        int previousBarrier = selectedBarrier;

        if (selectedBarrier > 0)
        {
            selectedBarrier--;
            previousBarrier = selectedBarrier;
        }
        return previousBarrier;
    }

    #endregion

    #region Guns
    public int Selected_Gun(int selectedCar)
    {       
        return carData[selectedCar].Selected_Gun();
    }
    public bool IsGunLocked(int carNumber, int gunNumber)
    {
        return carData[carNumber].Is_Gun_Locked(gunNumber);
    }
    public bool IsUsedGun(int carNumber, int gunNumber)
    {
        return carData[carNumber].Is_Gun_Used(gunNumber);
    }
    public void Set_Unlocked_Gun(int carNumber, int gunNumber)
    {
        carData[carNumber].Set_Unlocked_Gun(gunNumber);
    }
    public void Use_Gun(int carNumber, int gunNumber)
    {
        carData[carNumber].Use_Gun(gunNumber);
    }
    public int Get_Next_Gun(int carNumber, int selectedGun)
    {
        int nextGun = selectedGun;

        if (selectedGun < carData[carNumber].guns.Count - 1)
        {
            selectedGun++;
            nextGun = selectedGun;
        }
        return nextGun;
    }
    public int Get_Previous_Gun(int carNumber, int selectedGun)
    {
        int previousGun = selectedGun;

        if (selectedGun > 0)
        {
            selectedGun--;
            previousGun = selectedGun;
        }
        return previousGun;
    }
    public int Get_Gun_Price(int carNumber, int gunNumber)
    {
        int price = 0;

        price = carData[carNumber].Get_Gun_Price(gunNumber);
        return price;
    }
    public string Get_Gun_Name(int carNumber, int gunNumber)
    {
        return carData[carNumber].Get_Gun_Name(gunNumber);
    }

    #endregion

    #region Missiles

    public int Selected_Missile(int selectedCar)
    {
        int selected = 0;
        selected = carData[selectedCar].Selected_Missile();
        return selected;
    }
    public bool IsMissileLocked(int carNumber, int missileNumber)
    {
        bool is_Locked = false;

        is_Locked = carData[carNumber].Is_Missile_Locked(missileNumber);

        return is_Locked;
    }
    public void Set_Unlocked_Missile(int carNumber, int missileNumber)
    {
        carData[carNumber].Set_Unlocked_Missile(missileNumber);
    }
    public void Use_Missile(int carNumber, int missileNumber)
    {
        carData[carNumber].Use_Missile(missileNumber);
    }
    public int Get_Next_Missile(int carNumber, int selectedMissile)
    {
        int nextMissile = selectedMissile;

        if (selectedMissile < carData[carNumber].missiles.Count - 1)
        {
            selectedMissile++;
            nextMissile = selectedMissile;
        }
        return nextMissile;
    }
    public int Get_Previous_Missile(int carNumber, int selectedMissile)
    {
        int previousMissile = selectedMissile;

        if (selectedMissile > 0)
        {
            selectedMissile--;
            previousMissile = selectedMissile;
        }
        return previousMissile;
    }
    public int Get_Missile_Price(int carNumber, int missileNumber)
    {
        int price = 0;

        price = carData[carNumber].Get_Missile_Price(missileNumber);
        return price;
    }

    #endregion

    #region Car Camera
    public float CameraHeight(int carNumber)
    {
        return carData[carNumber].cameraHeight;
    }
    public float CameraRadius(int carNumber)
    {
        return carData[carNumber].cameraRadius;
    }

    #endregion

}

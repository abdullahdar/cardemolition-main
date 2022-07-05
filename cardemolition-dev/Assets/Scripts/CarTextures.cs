using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CarTextures : ScriptableObject
{
    [System.Serializable]
    public class Cars
    {
        public string name;
        public Sprite carTexture;
        public Material material;

        [System.Serializable]
        public class Guns
        {
            public string name;
            public Sprite texture;
            public Texture gunTexture;
        }
        public List<Guns> guns;        
    }
    public List<Cars> cars;

    public Sprite GetCarTexture(int carNumber)
    {
        Sprite sprite = null;

        sprite = cars[carNumber].carTexture;
        return sprite;
    }
    public Sprite GetGunTexture(int carNumber, int gunNumber)
    {
        Sprite sprite = null;

        sprite = cars[carNumber].guns[gunNumber].texture;
        return sprite;
    }
    public void SetMaterial(int carNo, int gunNo)
    {
        cars[carNo].material.mainTexture = cars[carNo].guns[gunNo].gunTexture;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Gun")]
public class GunAsset : ScriptableObject {
    
    public GameObject model;
    public Vector3 posOffset;
    public GunStats gunStats;
    

}

[System.Serializable]
public class GunStats
{
    public float fireRate;
    public float recoil;
    public float damage;
}
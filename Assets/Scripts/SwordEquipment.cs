using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEquipment : MonoBehaviour
{

    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;

    private void Start()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
    }

    public void DrawWeapon()
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
    }

    public void StartDealingDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealing>().StartDealingDamage();
    }

    public void EndDealingDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealing>().EndDealingDamage();
    }

}

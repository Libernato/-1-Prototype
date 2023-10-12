using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AxeDamageDeal : MonoBehaviour
{

    private bool canDealDamage;
    private bool hasDealDamage;

    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    [SerializeField] LayerMask playerMask;

    void Start()
    {
        canDealDamage = false;
        hasDealDamage = false;
    }
    void Update()
    {
        if(canDealDamage && !hasDealDamage)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, -transform.forward, out hit, weaponLength, playerMask))
            {
                if(hit.transform.TryGetComponent(out PlayerHealth playerHealth))
                {
                    playerHealth.TakeDamage(weaponDamage);
                    playerHealth.HitVFX(hit.point);
                    Debug.Log("Hitted");
                    hasDealDamage = true;
                }
            }

        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealDamage = false;
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * weaponLength);   
    }
}

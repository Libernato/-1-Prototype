using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DamageDealing : MonoBehaviour
{
    
    public LayerMask enemyMask;
    private bool _canDealDamage;
    private List<GameObject> _hasDealtDamage;

    [SerializeField] float weapomLength;
    [SerializeField] float weapomDamage;

    private void Start()
    {
        _canDealDamage = false;
        _hasDealtDamage = new List<GameObject>();    
    }

    private void Update()
    {
        if(_canDealDamage)
        {
            RaycastHit hit;
            
            if(Physics.Raycast(transform.position, -transform.forward, out hit, weapomLength, enemyMask))
            {
                if(hit.transform.TryGetComponent(out Enemy enemy) && !_hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    enemy.TakeDamage(weapomDamage);
                    enemy.HitVFX(hit.point);
                    print("Attacked! " + weapomDamage);
                    _hasDealtDamage.Add(hit.transform.gameObject);
                }
            }

        }    
    }

    public void StartDealingDamage()
    {
        _canDealDamage = true;
        _hasDealtDamage.Clear();
    }

    public void EndDealingDamage()
    {
        _canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * weapomLength);        
    }

}

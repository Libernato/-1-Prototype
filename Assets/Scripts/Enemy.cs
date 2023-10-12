using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] float health;
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float aggroRange = 5f;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    private GameObject _player;
    private Animator _animator;
    private NavMeshAgent _agent;
    private float timePassed;
    private float newDestionationCD = 0.5f;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        _animator.SetFloat("speed", _agent.velocity.magnitude / _agent.speed);

        if(_player == null)
        {
            return;
        }

        if(timePassed >= attackCD)
        {
            if(Vector3.Distance(_player.transform.position, transform.position) <= attackRange)
            {
                _animator.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;
        if(newDestionationCD <= 0 && Vector3.Distance(_player.transform.position, transform.position) <= aggroRange)
        {
            newDestionationCD = 0.5f;
            _agent.SetDestination(_player.transform.position);
            transform.LookAt(_player.transform);
        }

        newDestionationCD -= Time.deltaTime;

    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        _animator.SetTrigger("takeDamage");
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Debug.Log("Enemy died!");
        Destroy(this.gameObject);
    }

    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, transform.rotation);
        Destroy(hit, 3f);
    }

    public void StartDealingDamage()
    {
        GetComponentInChildren<AxeDamageDeal>().StartDealDamage();
    }

    public void EndDealingDamage()
    {
        GetComponentInChildren<AxeDamageDeal>().EndDealDamage();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);    
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject playerRagdoll;
    float currentHealth;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        _anim.SetTrigger("TakeDamage");

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(playerRagdoll, transform.position, transform.rotation);
        Debug.Log("Player died(");
        Destroy(this.gameObject);
    }

    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, transform.rotation);
        Destroy(hit, 3f);
    }

}

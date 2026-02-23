using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Behaviour[] components;

    public BossHealthBar healthBar;
	public int maxHealth = 10;
    public int currentHealth;
	public bool isInvulnerable = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
		{
			TakeDamage(1);
		}
    }

	public void TakeDamage(int _damage)
	{
		if (isInvulnerable)
			return;

		currentHealth -= _damage;

		if (currentHealth <= 4)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		anim.SetTrigger("die");

        //Deactivate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = false;
	}

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
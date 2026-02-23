using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Animator anim;
	public int maxHealth = 100;
	public int currentHealth;
	public bool isInvulnerable = false;
	public BossHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.Space))
		{
			TakeDamage(0);
		}
    }

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	
		if (currentHealth > 0){
			{
				anim.SetTrigger("hurt");
				anim.ResetTrigger("hurt");
			}

			if (currentHealth <= 4)
			{
				GetComponent<Animator>().SetBool("IsEnraged", true);
			}

			if (currentHealth <= 0)
			{
				Die();
			}      
		}

	}


	void Die()
	{
        anim.SetTrigger("die");
	}

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
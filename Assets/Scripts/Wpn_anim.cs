using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wpn_anim : MonoBehaviour
{
	public Animator w_Animator;
	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;
	
	

	void Start()
	{
		w_Animator = GetComponent<Animator>();
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
	}

	public void PAttack()
	{
		w_Animator.SetTrigger("Attack");
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
		foreach (Collider2D enemy in hitEnemies)
		{
			RaycastHit2D enemyCheck = Physics2D.Raycast(attackPoint.position, attackPoint.position - enemy.transform.position, Mathf.Clamp(attackRange, 0, Vector3.Distance(attackPoint.position, enemy.transform.position)));
			if(enemyCheck.collider == null)
			{
				enemy.GetComponent<EnemyScript>().Death();
				Debug.Log("Hit " + enemy.name);
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
		{
			return;
		}
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}

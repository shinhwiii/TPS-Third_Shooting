using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider HPBar;

    private float enemyMaxHP = 10;
    public float enemyCurHP = 0;

    private NavMeshAgent agent;
    private Animator anim;

    private GameObject target;
    private float targetDelay = 0.5f;

    private CapsuleCollider capsuleCollider;

    public void EnemyStart()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = true;

        target = GameObject.FindWithTag("Player");

        InitEnemy();
    }

    private void Update()
    {
        HPBar.value = enemyCurHP / enemyMaxHP;

        if (enemyCurHP <= 0)
        {
            StartCoroutine(EnemyDie());
            return;
        }

        if (target != null)
        {
            float maxDelay = 0.5f;
            targetDelay += Time.deltaTime;

            if (targetDelay < maxDelay)
            {
                return;
            }

            agent.destination = target.transform.position;
            transform.LookAt(target.transform.position);

            bool isRange = Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance;

            if (isRange)
            {
                anim.SetTrigger("Attack");
            }
            else
            {
                anim.SetFloat("MoveSpeed", agent.velocity.magnitude);
            }

            targetDelay = 0;
        }
    }

    private void InitEnemy()
    {
        enemyCurHP = enemyMaxHP;

        agent.speed = 1;
        capsuleCollider.enabled = true;
    }

    private IEnumerator EnemyDie()
    {
        agent.speed = 0;
        anim.SetTrigger("Dead");
        capsuleCollider.enabled = false;

        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);

        InitEnemy();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider HPBar;

    private float enemyMaxHP = 10;
    public float enemyCurHP = 0;

    private void Start()
    {
        InitEnemyHP();
    }

    private void Update()
    {
        HPBar.value = enemyCurHP / enemyMaxHP;
    }

    private void InitEnemyHP()
    {
        enemyCurHP = enemyMaxHP;
    }
}

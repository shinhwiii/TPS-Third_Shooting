using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyMaxHP = 100;
    public int enemyCurHP = 0;

    private void Start()
    {
        InitEnemyHP();
    }

    private void InitEnemyHP()
    {
        enemyCurHP = enemyMaxHP;
    }
}

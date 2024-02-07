using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Bullet")]
    [SerializeField]
    private Transform bulletPoint;
    [SerializeField]
    private GameObject bulletObj;
    [SerializeField]
    private float maxShootDelay = 0.2f;
    [SerializeField]
    private float curShootDelay;
    [SerializeField]
    private TextMeshProUGUI bulletText;
    [SerializeField]
    private GameObject aimImage;
    private int maxBullet = 30;
    private int curBullet = 30;

    [Header("Weapon FX")]
    [SerializeField]
    private GameObject weaponFlashFX;
    [SerializeField]
    private Transform bulletCasePoint;
    [SerializeField]
    private GameObject bulletCaseFX;
    [SerializeField]
    private Transform weaponClipPoint;
    [SerializeField]
    private GameObject weaponClipFX;

    [Header("Enemy")]
    [SerializeField]
    private GameObject[] enemySpawnPoint;

    [Header("BGM")]
    [SerializeField]
    private AudioClip bgmSound;
    private AudioSource bgmSource;

    private PlayableDirector cut;
    public bool isReady = true;

    private void Start()
    {
        instance = this;

        curShootDelay = 0;

        cut = GetComponent<PlayableDirector>();
        cut.Play();
    }

    private void Update()
    {
        bulletText.text = curBullet.ToString() + " / " + maxBullet.ToString();

        curShootDelay += Time.deltaTime;
    }

    public void Shooting(Vector3 targetPos, Enemy enemy, AudioSource weaponSource, AudioClip shootingSound)
    {
        if (curShootDelay < maxShootDelay || curBullet <= 0)
        {
            return;
        }

        curBullet--;
        curShootDelay = 0;

        weaponSource.clip = shootingSound;
        weaponSource.Play();

        Vector3 aimDis = (targetPos - bulletPoint.position).normalized;

        GameObject flashFX = PoolManager.instance.ActivateObj(1);
        SetObjPosition(flashFX, bulletPoint);
        flashFX.transform.rotation = Quaternion.LookRotation(aimDis, Vector3.up);

        GameObject caseFX = PoolManager.instance.ActivateObj(2);
        SetObjPosition(caseFX, bulletCasePoint);

        GameObject bulletFX = PoolManager.instance.ActivateObj(0);
        SetObjPosition(bulletFX, bulletPoint);
        bulletFX.transform.rotation = Quaternion.LookRotation(aimDis, Vector3.up);
    }

    public void ReloadClip()
    {
        GameObject clipFX = PoolManager.instance.ActivateObj(3);
        SetObjPosition(clipFX, weaponClipPoint);

        StartCoroutine(InitBullet());
    }

    private IEnumerator InitBullet()
    {
        yield return new WaitForSeconds(1f);

        curBullet = maxBullet;
    }

    private void SetObjPosition(GameObject obj, Transform targetPos)
    {
        obj.transform.position = targetPos.position;
    }

    private IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(2f);

        GameObject enemy = PoolManager.instance.ActivateObj(4);

        SetObjPosition(enemy, enemySpawnPoint[Random.Range(0, enemySpawnPoint.Length)].transform);

        enemy.GetComponent<Enemy>().EnemyStart();

        StartCoroutine(EnemySpawn());
    }

    private void PlayBGMSound()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.clip = bgmSound;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StartGame()
    {
        isReady = false;

        bulletText.enabled = true;
        aimImage.SetActive(true);

        PlayBGMSound();

        StartCoroutine(EnemySpawn());
    }
}

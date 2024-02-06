using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    private void Start()
    {
        Instance = this;

        curShootDelay = 0;
    }

    private void Update()
    {
        bulletText.text = curBullet.ToString() + " / " + maxBullet.ToString();
    }

    public void Shooting(Vector3 targetPos)
    {
        curShootDelay += Time.deltaTime;

        if (curShootDelay < maxShootDelay || curBullet <= 0)
        {
            return;
        }

        curBullet--;
        curShootDelay = 0;

        Instantiate(weaponFlashFX, bulletPoint);
        Instantiate(bulletCaseFX, bulletCasePoint);

        Vector3 aim = (targetPos - bulletPoint.position).normalized;
        Instantiate(bulletObj, bulletPoint.position, Quaternion.LookRotation(aim, Vector3.up));
    }

    public void ReloadClip()
    {
        StartCoroutine(InitBullet());
        Instantiate(weaponClipFX, weaponClipPoint);
    }

    private IEnumerator InitBullet()
    {
        yield return new WaitForSeconds(1f);

        curBullet = maxBullet;
    }
}

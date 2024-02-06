using System.Collections;
using TMPro;
using UnityEngine;

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
        instance = this;

        curShootDelay = 0;
    }

    private void Update()
    {
        bulletText.text = curBullet.ToString() + " / " + maxBullet.ToString();

        curShootDelay += Time.deltaTime;
    }

    public void Shooting(Vector3 targetPos, Enemy enemy)
    {
        if (curShootDelay < maxShootDelay || curBullet <= 0)
        {
            return;
        }

        curBullet--;
        curShootDelay = 0;
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
}

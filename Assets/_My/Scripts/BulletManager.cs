using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody bulletRigid;

    [SerializeField]
    private float moveSpeed = 10f;
    private float destroyTime = 3f;

    private void Start()
    {
        bulletRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
            DestroyBullet();
        }

        BulletMove();
    }

    private void BulletMove()
    {
        bulletRigid.velocity = transform.forward * moveSpeed;
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
        destroyTime = 3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyBullet();
    }
}

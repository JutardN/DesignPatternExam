using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    List<Bullet> bullets;
    [SerializeField] Bullet _bulletPrefab;

    private void Start()
    {
        bullets = new List<Bullet>();
    }

    public void SpawnBullet(Transform _spawnPoint, int power)
    {
        // CREATE BULLET //
        if (bullets.Count == 0)
        {
            Bullet b = Instantiate(_bulletPrefab, _spawnPoint.transform.position, Quaternion.identity, null)
        .Init(_spawnPoint.TransformDirection(Vector3.right), power);
            b.SetBulletPool(this);
        }
        // REUSE BULLET //
        else
        {
            bullets[0].Init(_spawnPoint.TransformDirection(Vector3.right),power);
            bullets[0].transform.position = _spawnPoint.transform.position;
            bullets[0].gameObject.SetActive(true);
            bullets.RemoveAt(0);
        }
    }

    public void EndBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullets.Add(bullet);
    }
}

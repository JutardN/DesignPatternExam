using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFire : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] BulletPool _bulletPool;

    public void FireBullet(int power)
    {
        _bulletPool.SpawnBullet(_spawnPoint, power);
        //var b = Instantiate(_bulletPrefab, _spawnPoint.transform.position, Quaternion.identity, null)
        //    .Init(_spawnPoint.TransformDirection(Vector3.right), power);
    }

}

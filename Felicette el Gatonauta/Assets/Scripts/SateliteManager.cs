using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteManager : MonoBehaviour
{
    public static SateliteManager instance;

    ObjectPool<Bullet> _pool;
    BulletFactory _factory;

    public Bullet bulletPrefab;
    public int initialPoolStock = 18;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        _factory = new BulletFactory(bulletPrefab);
        _pool = new ObjectPool<Bullet>(_factory.Get, Bullet.TurnOn, Bullet.TurnOff, initialPoolStock);
    }

    public Bullet GetBullet()
    {
        //print("SATMANAGER: me pidieron una bala");
        Bullet b;
        b = _pool.GetObject();
        return b;
    }

    public void ReturnBullet(Bullet b)
    {
        //print("SATMANAGER: me dieron una bala para devolver");
        _pool.ReturnObject(b);
    }

}

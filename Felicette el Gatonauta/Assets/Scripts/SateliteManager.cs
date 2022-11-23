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

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(GetAndReturn());
        //}
    }

    public Bullet GetBullet()
    {
        print("SATMANAGER: me pidieron una bala");
        Bullet b;
        b = _pool.GetObject();
        return b;
    }

    public void ReturnBullet(Bullet b)
    {
        print("SATMANAGER: me dieron una bala para devolver");
        _pool.ReturnObject(b);
    }

    //public Bullet Factory()
    //{
    //    Bullet b;
    //    b = Instantiate(bulletPrefab, RandomPosition(), Quaternion.identity);
    //    b.name = "bullet " + b.GetInstanceID().ToString();
    //    return b;
    //}

    //IEnumerator GetAndReturn()
    //{
    //    Bullet b;
    //    b = _pool.GetObject();
    //    yield return new WaitForSeconds(1);
    //    _pool.ReturnObject(b);
    //}

    //public static Vector3 RandomPosition()
    //{
    //    Vector3 newPos;
    //    newPos.x = Random.Range(0, 10);
    //    newPos.y = Random.Range(0, 10);
    //    newPos.z = Random.Range(0, 10);
    //    return newPos;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class shooting : NetworkBehaviour
{
    
    public Transform firePoint;

    [SerializeField]
    public GameObject bulletpre;

    [SerializeField]
    public float bulletForce = 8f;

    private float bulletStockFull = 6f;
    private float bulletsStock = 6f;

    float timeLeft;

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0)
            timeLeft -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && this.isLocalPlayer)
        {
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {

        Debug.Log(bulletsStock);
        Debug.Log(timeLeft);

        if (bulletsStock > 0 && timeLeft <= 0)
        {
            
            Vector3 vec = new Vector3(firePoint.position.x - this.transform.position.x, firePoint.position.y - this.transform.position.y);

            GameObject bullet = Instantiate(bulletpre, this.transform.position + vec, firePoint.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * bulletForce;

            NetworkServer.Spawn(bullet);

            Destroy(bullet, 4.0f);

            dropBulltes();
        }
        else
        {

            if (timeLeft < 0)
                resetBulltes();
                
        }

    }

    void resetBulltes()
    {
        bulletsStock = bulletStockFull;
    }

    void dropBulltes()
    {
        bulletsStock -= 1f;
        if (bulletsStock == 0)
            timeLeft = 3.0f;
    }

}

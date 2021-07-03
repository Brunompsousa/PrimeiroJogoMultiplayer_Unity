using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Char_mov : NetworkBehaviour
{
    public Rigidbody2D player;
    public Camera cam;
    public float speed = 5f; //velocidade a que sera feito o movimento

    Vector2 moveDir; //Posicao para onde queremos que a personagem se mova
    Vector2 mousePos;

    private Vector2 initialPosition;

    void Start()
    {
        this.initialPosition = this.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.isLocalPlayer)
        {

            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.y = Input.GetAxisRaw("Vertical");

            player.MovePosition(player.position + moveDir * speed * Time.fixedDeltaTime);

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 lookDir = mousePos - player.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            player.rotation = angle;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.collider.name);
        if (collision.collider.tag == "Bullet"&& !this.isLocalPlayer)
        {
            Destroy(this.gameObject);
            //Debug.Log("DIE");
        }
    }

    public void dead()
    {

        RpcRespawn();

    }

    [ClientRpc]
    void RpcRespawn()
    {
        this.transform.position = this.initialPosition;
    }

}



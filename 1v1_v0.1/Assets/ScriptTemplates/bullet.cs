using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class bullet : NetworkBehaviour
{
    // Funcao chamada quando a bala colide com algo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroi o objeto, neste caso a bala
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Guarda o inimigo que foi atingido na variavel 
        Char_mov target = hitInfo.GetComponent<Char_mov>();

         //Caso o target nao seja null vai chamar a funcao para dar dano ao inimigo e dps destroi a bala
        if(target != null)
        {
            if(!target.isLocalPlayer)
            { 
                target.dead();
                Destroy(gameObject);
            }
        }
    }
}

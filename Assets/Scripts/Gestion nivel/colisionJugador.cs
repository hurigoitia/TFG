using UnityEngine;

public class colisionJugador : MonoBehaviour
{

    public GameManager gameManager;

    /*
    Si el jugador entra en contacto con la lava, se reinicia la partida.
    Si el jugador entra en la meta, se completa el nivel actual y se avanza al sigueinte.
    */
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "lava")
        {
            gameManager.EndGame();

        }else if (collisionInfo.collider.tag == "meta"){
            gameManager.CompleteLevel();
        }
    }
}

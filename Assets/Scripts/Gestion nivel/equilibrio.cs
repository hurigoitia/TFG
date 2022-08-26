using UnityEngine;
using UnityEngine.UI;

public class equilibrio : MonoBehaviour
{
    
    public Transform jugador;
    public Text equilibrioTexto;
    private double valorEquilibrio = 100f;
    public GameManager gameManager;
    private bool detenerPartida = false;
    public double perdidaEquilibrio = 0.1f;

    /* Comprueba que debajo del jugador siempre haya un pilar. Si no es así, el contador de
    equilibrio comenzará a reducirse */
    private void Update()
    {
        Ray ray = new Ray (jugador.transform.position, -transform.up);

        if (!detenerPartida && !Physics.Raycast(ray, 100, 1 << 7) && !Physics.Raycast(ray, 100, 1 << 8))
        {
            //print("EL JUGADOR ESTA ENCIMA DE UN PILAR O DEL ASCENSOR");
            valorEquilibrio = valorEquilibrio - perdidaEquilibrio;
            equilibrioTexto.text = "Equilibrio: " + valorEquilibrio.ToString("0") + "%";
        }

        //Si el valor del equilibrio es menor que 0, se reinicia la partida

        if (valorEquilibrio <= 0){
            detenerPartida = true;
            gameManager.EndGame();
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour{
    
    bool partidaTerminada = false;
    public float tiempoDemora = 1f;
    public GameObject completeLevelUI;

    /*
    Cuando se completa un nivel, se muestra el canvas de nivel completado.
    */
    public void CompleteLevel(){
        completeLevelUI.SetActive(true);
    }

    /*
    Cuando la llave o el jugador entran en contacto con la lava o cuando el contador del equilibrio 
    llega a 0, se reinicia el nivel.
    */
    public void EndGame (){
        if (partidaTerminada == false){
            partidaTerminada = true;
            Invoke("Restart", tiempoDemora);
        }
    }

    /*
    Reinicia el nivel actual.
    */
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
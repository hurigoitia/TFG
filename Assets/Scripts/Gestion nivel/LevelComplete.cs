using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class LevelComplete : MonoBehaviour{
    
    /*
    Si el jugador termina el nivel 1 (tutorial) se carga el nivel 0 (menu).
    Si el jugador termina el nivel 4, se carga el nivel 0.
    En cualquier otro caso, se carga el siguiente nivel. En este caso se reestablecen los ficheros de la posición del fantasma.
    */
    public void LoadNextLevel(){
        if (SceneManager.GetActiveScene().buildIndex == 1){
            SceneManager.LoadScene(0);
        }else if (SceneManager.GetActiveScene().buildIndex == 4){
            File.WriteAllText(Application.persistentDataPath + "/dataSave.json", string.Empty);
            File.WriteAllText(Application.persistentDataPath + "/dataSaveCaja.json", string.Empty);
            SceneManager.LoadScene(0);
        }else{
            File.WriteAllText(Application.persistentDataPath + "/dataSave.json", string.Empty);
            File.WriteAllText(Application.persistentDataPath + "/dataSaveCaja.json", string.Empty);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
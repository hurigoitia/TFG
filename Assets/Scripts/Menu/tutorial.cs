using UnityEngine.SceneManagement;
using UnityEngine;

public class tutorial : MonoBehaviour
{

    void OnTriggerEnter(Collider llave){
        if (llave.gameObject.CompareTag("llave")){
            comenzarTutorial();
        }
    }

    public void comenzarTutorial(){
        SceneManager.LoadScene(1);
    }
}

using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Text;

public class jugar : MonoBehaviour
{   

    void OnTriggerEnter(Collider llave){
        if (llave.gameObject.CompareTag("llave")){
            comenzarPartida();
        }
    }

    public void comenzarPartida(){
        File.WriteAllText(Application.persistentDataPath + "/dataSave.json", string.Empty);
        File.WriteAllText(Application.persistentDataPath + "/dataSaveCaja.json", string.Empty);
        SceneManager.LoadScene(2);
    }
}

using UnityEngine;

public class aperturaPuerta : MonoBehaviour{
    public GameObject puerta;

    void OnTriggerEnter(Collider llave){
        if (llave.gameObject.CompareTag("llave")){
            puerta.GetComponent<Animation>().Play();
            puerta.GetComponent<AudioSource>().Play();
        }
    }
}

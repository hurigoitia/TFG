using UnityEngine;

public class creacionPuente : MonoBehaviour
{
    public GameObject puente;

    /* 
    Cuando la llave entra en contacto con el altar, se activan las animaciones y el sonido del puente
    */
    void OnTriggerEnter(Collider llave){
        if (llave.gameObject.CompareTag("llave")){
            puente.GetComponent<Animation>().Play();
            puente.GetComponent<AudioSource>().Play();
        }
    }
}

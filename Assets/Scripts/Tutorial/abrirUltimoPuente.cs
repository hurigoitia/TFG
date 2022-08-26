using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abrirUltimoPuente : MonoBehaviour
{
    public GameObject puente2;

    void OnTriggerEnter(Collider llave){
        if (llave.gameObject.CompareTag("llave")){
            puente2.GetComponent<Animation>().Play();
            puente2.GetComponent<AudioSource>().Play();
        }
    }
}

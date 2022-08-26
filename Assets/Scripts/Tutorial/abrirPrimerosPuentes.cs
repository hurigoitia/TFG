using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abrirPrimerosPuentes : MonoBehaviour
{
    public GameObject puente1;
    public GameObject puente3;

    void OnTriggerEnter(Collider llave){
        if (llave.gameObject.CompareTag("llave")){
            puente1.GetComponent<Animation>().Play();
            puente1.GetComponent<AudioSource>().Play();
            puente3.GetComponent<Animation>().Play();
        }
    }
}

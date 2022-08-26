using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilaresAgrietados : MonoBehaviour
{

    private int count = 3;
    public GameObject pilarArreglado;
    public ParticleSystem particulas;
    public GameObject jugador;
    public AudioSource sonidoPilarRoto;

    void OnTriggerEnter(Collider martillo){

        if (martillo.gameObject.CompareTag("martillo")){
            count -= 1;
            martillo.GetComponent<AudioSource>().Play();
            if (count == 0){
                pilarArreglado.SetActive(true);
                this.gameObject.SetActive(false);
           }
        }
    }

    public void Update(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit)){
            if (hit.transform.gameObject == jugador){
                particulas.Play();
                this.gameObject.SetActive(false);
                sonidoPilarRoto.Play();
            }
        }
    }
}

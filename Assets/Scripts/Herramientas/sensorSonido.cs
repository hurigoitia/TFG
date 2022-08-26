using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorSonido : MonoBehaviour
{

    public GameObject meta;
    public Transform esferaCercana;
    public Transform esferaMediana;
    public Transform esferaLejana;
    public AudioSource sonidoEsferaCercana;
    public AudioSource sonidoEsferaMediana;
    public AudioSource sonidoEsferaLejana;
    public GameObject particulasEsferaCercana;
    public GameObject particulasEsferaMediana;
    public GameObject particulasEsferaLejana;

    void OnTriggerEnter(Collider esfera){

        if (esfera.gameObject.CompareTag("esferaCercana")){
            sonidoEsferaMediana.Stop();
            particulasEsferaMediana.SetActive(false);
            sonidoEsferaLejana.Stop();
            particulasEsferaLejana.SetActive(false);
            sonidoEsferaCercana.Play();
            particulasEsferaCercana.SetActive(true);
        }

        if (esfera.gameObject.CompareTag("esferaMediana")){
            sonidoEsferaCercana.Stop();
            particulasEsferaCercana.SetActive(false);
            sonidoEsferaLejana.Stop();
            particulasEsferaLejana.SetActive(false);
            sonidoEsferaMediana.Play();
            particulasEsferaMediana.SetActive(true);
        }
        
        if (esfera.gameObject.CompareTag("esferaLejana")){
            sonidoEsferaCercana.Stop();
            particulasEsferaCercana.SetActive(false);
            sonidoEsferaMediana.Stop();
            particulasEsferaMediana.SetActive(false);
            sonidoEsferaLejana.Play();
            particulasEsferaLejana.SetActive(true);

        }
    }

    void OnTriggerExit(Collider esfera){

        if (esfera.gameObject.CompareTag("esferaCercana")){
            particulasEsferaCercana.SetActive(false);
            sonidoEsferaCercana.Stop();
            particulasEsferaLejana.SetActive(false);
            sonidoEsferaLejana.Stop();
            particulasEsferaMediana.SetActive(true);
            sonidoEsferaMediana.Play();
        }

        if (esfera.gameObject.CompareTag("esferaMediana")){
            particulasEsferaMediana.SetActive(false);
            sonidoEsferaMediana.Stop();
            particulasEsferaLejana.SetActive(true);
            sonidoEsferaLejana.Play();
        }
    }
}

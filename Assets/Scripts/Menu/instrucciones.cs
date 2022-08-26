using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instrucciones : MonoBehaviour
{
    public GameObject instruccionesUI;
    public GameObject menuUI;
    private bool mostrarMenuInstrucciones = true;

    void OnTriggerEnter(Collider llave){
        if (llave.gameObject.CompareTag("llave")){
            mostrarInstrucciones();
        }
    }


    public void mostrarInstrucciones(){
        if (mostrarMenuInstrucciones){
            instruccionesUI.SetActive(true);
            menuUI.SetActive(false); 
            mostrarMenuInstrucciones = false;   
        }else{
            menuUI.SetActive(true);
            instruccionesUI.SetActive(false);
            mostrarMenuInstrucciones = true;
        }
    }
}

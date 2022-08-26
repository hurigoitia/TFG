using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Exception = System.Exception;

public class TutorialBoxJSONManager : MonoBehaviour
{   
    public GameObject cajaFantasma;
    private int count = 0;
    private bool noCargar = false;

    //Se crea una clase que representa la información relevante del jugador (posición, rotación y el momento en el que se lleva esa acción desde el inicio de la partida)
    [System.Serializable]
    public class DatoCaja {
        public Vector3 posicioncaja;
        public Quaternion rotacionCaja;
        //public float tiempoDelCambio;
    }

    //Se crea una clase que almacena todas las instancias de la clase DatoCaja
    [System.Serializable]
    public class ListaDatosCaja{
        public List<DatoCaja> Datos = new List<DatoCaja>();
    }


    /* Update is called once per frame
       Actualiza la información del cajaFantasma (desde el fichero /dataRead.json) y guarda la información del jugador (en el fichero /dataSave.json)
       La variable count se utiliza para visitar una sola posición del array JSON cargado desde el fichero /dataRead.json en cada
       actualización */
    void Update()
    {   
        Load(count);
        count++;
    }


    /* Carga toda la información del fichero JSON en el que se han guardado las coordenadas visitadas por el jugador
       en la partida anterior y las va cargando en cada actualización en el cajaFantasma.
       Si el contador es mayor que el índice maximo del array JSON se deja de sacar posiciones del array y se desactiva
       el modelo del cajaFantasma para que despaarezca de la partida.*/
    void Load(int count){
        if (!noCargar){
            string path = File.ReadAllText("Assets/Resources/dataTutorialCaja.json"); 
            ListaDatosCaja listaDatosCargar = JsonUtility.FromJson<ListaDatosCaja>(path);
            try {
                cajaFantasma.transform.position = listaDatosCargar.Datos[count].posicioncaja;
                cajaFantasma.transform.rotation = listaDatosCargar.Datos[count].rotacionCaja;
            }
            catch (Exception e) {
                Debug.Log("No hay ningún objeto en la posición actual de la variable count: " + e);
                noCargar = true;
                cajaFantasma.SetActive(false);
            } 
        }
    }
}

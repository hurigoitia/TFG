using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Exception = System.Exception;

public class TutorialPlayerJSONManager : MonoBehaviour
{   
    public GameObject fantasma;
    private int count = 0;
    private bool noCargar = false;
    public float alturaFantasma = -0.4f;

    //Se crea una clase que representa la información relevante del jugador (posición, rotación y el momento en el que se lleva esa acción desde el inicio de la partida)
    [System.Serializable]
    public class DatoJugador {
        public Vector3 posicionJugador;
        public float rotacionJugador;
        //public float tiempoDelCambio;
    }

    //Se crea una clase que almacena todas las instancias de la clase DatoJugador
    [System.Serializable]
    public class ListaDatosJugador{
        public List<DatoJugador> Datos = new List<DatoJugador>();
    }


    /* Update is called once per frame
       Actualiza la información del fantasma (desde el fichero /dataRead.json) y guarda la información del jugador (en el fichero /dataSave.json)
       La variable count se utiliza para visitar una sola posición del array JSON cargado desde el fichero /dataRead.json en cada
       actualización */
    void Update()
    {   
        Load(count);
        count++;
    }


    /* Carga toda la información del fichero JSON en el que se han guardado las coordenadas visitadas por el jugador
       en la partida anterior y las va cargando en cada actualización en el fantasma.
       Si el contador es mayor que el índice maximo del array JSON se deja de sacar posiciones del array y se desactiva
       el modelo del fantasma para que despaarezca de la partida.*/
    void Load(int count){
        if (!noCargar){
            string path = File.ReadAllText("Assets/Resources/dataTutorialPlayer.json"); 
            ListaDatosJugador listaDatosCargar = JsonUtility.FromJson<ListaDatosJugador>(path);
            try {
                Vector3 posicionTemporal = listaDatosCargar.Datos[count].posicionJugador;
                Quaternion rotacionTemporal = fantasma.transform.rotation;
                posicionTemporal.y = alturaFantasma;
                rotacionTemporal.y = listaDatosCargar.Datos[count].rotacionJugador;
                fantasma.transform.position = posicionTemporal;
                fantasma.transform.rotation = rotacionTemporal;
            }
            catch (Exception e) {
                Debug.Log("No hay ningún objeto en la posición actual de la variable count: " + e);
                noCargar = true;
                fantasma.SetActive(false);
            } 
        }
    }
}

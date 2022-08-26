using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Exception = System.Exception;

public class PlayerJSONManager : MonoBehaviour
{   
    public Transform jugador;
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

    ListaDatosJugador listaTodosLosDatos = new ListaDatosJugador();
    

    /*Elimina la información del fichero /dataRead.json de la partida anterior
      Hace una copia de la información de la partida anterior del fichero /dataSave.json en el fichero /dataRead.json
      Si después de estos cambios el fichero /dataRead.json está vacío, no se activa el modelo del fantasma ya que no hay
      información que representar. Esta situación ocurre en las primeras partidas. */
    void Start()
    {   
        File.WriteAllText(Application.persistentDataPath + "/dataRead.json", string.Empty);
        File.Copy(Path.Combine(Application.persistentDataPath + "/dataSave.json"), Path.Combine(Application.persistentDataPath + "/dataRead.json"), true);
        if (new FileInfo(Path.Combine(Application.persistentDataPath + "/dataRead.json")).Length != 0){
            fantasma.SetActive(true);
        }
    }


    /* Update is called once per frame
       Actualiza la información del fantasma (desde el fichero /dataRead.json) y guarda la información del jugador (en el fichero /dataSave.json)
       La variable count se utiliza para visitar una sola posición del array JSON cargado desde el fichero /dataRead.json en cada
       actualización */
    void Update()
    {   
        Save();
        Load(count);
        count++;
    }


    /* Va guardando en cada actualización la posición y la rotación en el eje y del jugador (cámara) desde el inicio de la partida.
       Esta información se guarda en C:\Users\*\AppData\LocalLow\DefaultCompany\TFG\dataSave.json en formato JSON*/
    private void Save(){

       string path = Application.persistentDataPath + "/dataSave.json";
        
        DatoJugador dato = new DatoJugador {
            posicionJugador = jugador.position,
            rotacionJugador = jugador.rotation.y
        };

        listaTodosLosDatos.Datos.Add(dato);
        File.WriteAllText(path, JsonUtility.ToJson(listaTodosLosDatos));
    }


    /* Carga toda la información del fichero JSON en el que se han guardado las coordenadas visitadas por el jugador
       en la partida anterior y las va cargando en cada actualización en el fantasma.
       Si el contador es mayor que el índice maximo del array JSON se deja de sacar posiciones del array y se desactiva
       el modelo del fantasma para que despaarezca de la partida.*/
    void Load(int count){
        if (!noCargar){
            string path = File.ReadAllText(Application.persistentDataPath + "/dataRead.json"); 
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

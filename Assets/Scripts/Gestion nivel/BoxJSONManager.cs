using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Exception = System.Exception;

public class BoxJSONManager : MonoBehaviour
{   
    public Transform caja;
    public GameObject cajaFantasma;
    private int count = 0;
    private bool noCargar = false;

    //Se crea una clase que representa la información relevante del caja (posición, rotación y el momento en el que se lleva esa acción desde el inicio de la partida)
    [System.Serializable]
    public class Datocaja {
        public Vector3 posicioncaja;
        public Quaternion rotacioncaja;
        //public float tiempoDelCambio;
    }

    //Se crea una clase que almacena todas las instancias de la clase Datocaja
    [System.Serializable]
    public class ListaDatoscaja{
        public List<Datocaja> Datos = new List<Datocaja>();
    }

    ListaDatoscaja listaTodosLosDatos = new ListaDatoscaja();
    

    /*Elimina la información del fichero /dataReadCaja.json de la partida anterior
      Hace una copia de la información de la partida anterior del fichero /dataSaveCaja.json en el fichero /dataReadCaja.json
      Si después de estos cambios el fichero /dataReadCaja.json está vacío, no se activa el modelo del cajaFantasma ya que no hay
      información que representar. Esta situación ocurre en las primeras partidas. */
    void Start()
    {   
        File.WriteAllText(Application.persistentDataPath + "/dataReadCaja.json", string.Empty);
        File.Copy(Path.Combine(Application.persistentDataPath + "/dataSaveCaja.json"), Path.Combine(Application.persistentDataPath + "/dataReadCaja.json"), true);
        if (new FileInfo(Path.Combine(Application.persistentDataPath + "/dataReadCaja.json")).Length != 0){
            cajaFantasma.SetActive(true);
        }
    }


    /* Update is called once per frame
       Actualiza la información del cajaFantasma (desde el fichero /dataReadCaja.json) y guarda la información del caja (en el fichero /dataSaveCaja.json)
       La variable count se utiliza para visitar una sola posición del array JSON cargado desde el fichero /dataReadCaja.json en cada
       actualización */
    void Update()
    {   
        Save();
        Load(count);
        count++;
    }


    /* Va guardando en cada actualización la posición y la rotación en el eje y del caja (cámara) desde el inicio de la partida.
       Esta información se guarda en C:\Users\*\AppData\LocalLow\DefaultCompany\TFG\dataSaveCaja.json en formato JSON*/
    private void Save(){

       string path = Application.persistentDataPath + "/dataSaveCaja.json";
        
        Datocaja dato = new Datocaja {
            posicioncaja = caja.position,
            rotacioncaja = caja.rotation
        };

        listaTodosLosDatos.Datos.Add(dato);
        File.WriteAllText(path, JsonUtility.ToJson(listaTodosLosDatos));
    }


    /* Carga toda la información del fichero JSON en el que se han guardado las coordenadas visitadas por el caja
       en la partida anterior y las va cargando en cada actualización en el cajaFantasma.
       Si el contador es mayor que el índice maximo del array JSON se deja de sacar posiciones del array y se desactiva
       el modelo del cajaFantasma para que despaarezca de la partida.*/
    void Load(int count){
        if (!noCargar){
            string path = File.ReadAllText(Application.persistentDataPath + "/dataReadCaja.json"); 
            ListaDatoscaja listaDatosCargar = JsonUtility.FromJson<ListaDatoscaja>(path);
            try {
                cajaFantasma.transform.position = listaDatosCargar.Datos[count].posicioncaja;
                cajaFantasma.transform.rotation = listaDatosCargar.Datos[count].rotacioncaja;
            }
            catch (Exception e) {
                Debug.Log("No hay ningún objeto en la posición actual de la variable count: " + e);
                noCargar = true;
                cajaFantasma.SetActive(false);
            } 
        }
    }
}

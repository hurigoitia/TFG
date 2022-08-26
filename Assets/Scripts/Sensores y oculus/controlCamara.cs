using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlCamara : MonoBehaviour
{
    public Transform headSensor;
    public float mult = 2f;

    Vector3 pos;
    Vector3 prevPos;


    private void Start()
    {
        //this.transform.position = headSensor.position;
        /**/this.transform.position = new Vector3(headSensor.position.x, this.transform.position.y, headSensor.position.z);
        pos = headSensor.position;
        prevPos = pos;
    }
    private void Update()
    {

        /* Modifica la posicion de la camara respecto al sensor de posicion de las gafas.
        Si la variable mult es 2, la cámara se movera una distancia el doble de grande que
        el objeto asociado al sensor de posicion (en este caso, el objeto HeadQuest) */
        pos = headSensor.position;

        Vector3 displaceVec =  pos - prevPos;
        /**/displaceVec *= -mult;
        /**/displaceVec.y = 0;
        //this.transform.Translate(displaceVec * -mult, Space.Self);
        /**/this.transform.Translate(displaceVec, Space.Self);
        
        prevPos = pos;

        //Modificar la rotacion de la camara respecto al sensor de posicion de las gafas
        //Cambiar la rotaci�n -90 grados en el eje Z del sensor de posici�n de las gafas para que se vea bien en Unity
        this.transform.localRotation = headSensor.rotation;

        //this.transform.Rotate(new Vector3(0, 0, 90));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlLlave : MonoBehaviour
{
    public Transform llaveSensor;
    public float mult = 2f;

    Vector3 pos;
    Vector3 prevPos;


    private void Start()
    {
        //this.transform.position = llaveSensor.position;
        /**/this.transform.position = new Vector3(llaveSensor.position.x, this.transform.position.y, llaveSensor.position.z);
        pos = llaveSensor.position;
        prevPos = pos;
    }
    private void Update()
    {

        /* Modifica la posicion de la Llave respecto al sensor de posicion de las gafas.
        Si la variable mult es 2, la Llave se movera una distancia el doble de grande que
        el objeto asociado al sensor de posicion (en este caso, el objeto HeadQuest) */
        pos = llaveSensor.position;

        Vector3 displaceVec =  pos - prevPos;
        /**/displaceVec.x *= -mult;
        /**/displaceVec.z *= -mult;
        //this.transform.Translate(displaceVec * -mult, Space.Self);
        /**/this.transform.Translate(displaceVec, Space.Self);
        
        prevPos = pos;

        //Modificar la rotacion de la Llave respecto al sensor de posicion de las gafas
        //Cambiar la rotaci�n -90 grados en el eje Z del sensor de posici�n de las gafas para que se vea bien en Unity
        this.transform.localRotation = llaveSensor.rotation;

        //this.transform.Rotate(new Vector3(0, 0, 90));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colisionPuerta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(10, 7);
        Physics.IgnoreLayerCollision(10, 9);
    }
}

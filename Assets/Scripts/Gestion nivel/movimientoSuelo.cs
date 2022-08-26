using UnityEngine;

public class movimientoSuelo : MonoBehaviour
{
    public Rigidbody rb;
    public float velocidadSuelo;

    //El suelo de lava ignorará ciertas capas, como los pilares, para poder alcanzar hasta jugador.
    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, velocidadSuelo, 0);

        Physics.IgnoreLayerCollision(6, 0);
        Physics.IgnoreLayerCollision(6, 5);
        Physics.IgnoreLayerCollision(6, 7);
        Physics.IgnoreLayerCollision(6, 8);
        Physics.IgnoreLayerCollision(6, 9);
        Physics.IgnoreLayerCollision(6, 10);
        Physics.IgnoreLayerCollision(6, 11);
        Physics.IgnoreLayerCollision(6, 13);

    }
}
using UnityEngine;

public class movimientoAscensor : MonoBehaviour
{

    public GameObject ascensor;
    public float speed;
  
    public void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * 21 - 0;
        ascensor.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

}
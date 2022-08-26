using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limiteSeguridad : MonoBehaviour
{   
    public GameObject pared1;
    public GameObject pared2;
    public GameObject pared3;
    public GameObject pared4;
    public Transform player;

    // Update is called once per frame

    /*
    Cuando el jugador se acerca a alguno de los límites del suelo de sensores,
    se mostrará enfrente de este una pared transparente que indicará estos límites
    */
    void Update()
    {
        if (pared1.transform.position.z - player.position.z <= 2.0f){
            pared1.SetActive(true);
        }else{
            pared1.SetActive(false);
        }

        if (player.position.z - pared2.transform.position.z <= 1.0f){
            pared2.SetActive(true);
        }else{
            pared2.SetActive(false);
        }

        if (player.position.x - pared3.transform.position.x >= -1.5f){
            pared3.SetActive(true);
        }else{
            pared3.SetActive(false);
        }

        if (0.0f <= player.position.x - pared4.transform.position.x && player.position.x - pared4.transform.position.x <= 2.0f){
            pared4.SetActive(true);
        }else{
            pared4.SetActive(false);
        }
    }
}

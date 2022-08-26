
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Antilatency.OculusSample;
using Antilatency.DeviceNetwork;

public class TagBasedOculusAltTracking :  AltTrackingOculus
{


    [SerializeField]
    protected string _trackerTag;



    //Cambiamos el comportamiento por defecto de escoger el tracker conectado por usb para que coja el que queramos dado un tag.
    protected override NodeHandle GetAvailableTrackingNode()
    {
        return GetFirstIdleTrackerNodeBySocketTag(_trackerTag);
    }


}

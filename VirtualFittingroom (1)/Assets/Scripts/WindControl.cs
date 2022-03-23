using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class WindControl : MonoBehaviour
{
    float currentrotation = 0;
    public ObiAmbientForceZone windZone;
    // Start is called before the first frame update
    void Start()
    {
        //referi obin tuulikomponenttiin
        windZone = this.gameObject.GetComponent<ObiAmbientForceZone>();
        windZone.intensity = 0;
        windZone.turbulence = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //tuulensuunnan säätö
    public void UpdateRotation(float newvalue)
    {
        this.gameObject.transform.Rotate(0, newvalue - currentrotation, 0);
        currentrotation = newvalue;
    }

    //tuulenvoimakkuuden säätö
    public void UpdateWindStrength(float newvalue)
    {
        windZone.intensity = newvalue;
    }
    
    //tuulenvoimakkuuden säätö
    public void UpdateWindTurbulense(float newvalue)
    {
        windZone.turbulence = newvalue;
    }
}

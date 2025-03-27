using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public PseudoHaptics pseudoHaptics;
    //public PseudoHaptics PseudoHapticsL;
    public GameObject WeightR;
    public GameObject WeightL;
    

    Vector3 st_posR;
    Vector3 st_posL;
    

    // Start is called before the first frame update
    void Start()
    {
        st_posR = WeightR.transform.position;
        st_posL = WeightL.transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (pseudoHaptics.flag_reset)
        {
            if (pseudoHaptics.grab_count == 0)
            {
                
                pseudoHaptics.flag_reset = false;
                //PseudoHapticsL.flag_reset = false;
            }
            else
            {
                pseudoHaptics.flag_reset = false;
                //PseudoHapticsL.flag_reset = false;
            }
            
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "reset_pos")
        {
           
            WeightR.transform.position = st_posR;
            WeightL.transform.position = st_posL;
        }

    }
}

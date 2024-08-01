using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public PseudoHaptics PseudoHapticsR;
    public PseudoHaptics PseudoHapticsL;
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
        if (PseudoHapticsR.flag_reset || PseudoHapticsL.flag_reset)
        {
            if (PseudoHapticsR.grab_count == 0 && PseudoHapticsL.grab_count == 0)
            {
                WeightR.transform.position = st_posR;
                WeightL.transform.position = st_posL;
                PseudoHapticsR.flag_reset = false;
                PseudoHapticsL.flag_reset = false;
            }
            else
            {
                PseudoHapticsR.flag_reset = false;
                PseudoHapticsL.flag_reset = false;
            }
            
        }
    }
}

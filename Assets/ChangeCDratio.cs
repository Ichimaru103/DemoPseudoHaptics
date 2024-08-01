using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCDratio : MonoBehaviour
{
    // Start is called before the first frame update
    public PseudoHaptics PseudoHapticsR;
    public PseudoHaptics PseudoHapticsL;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Change right hand CDratio 
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (PseudoHapticsR.CD > 0.6f)
            {
                PseudoHapticsR.CD = PseudoHapticsR.CD - 0.1f;
            }

            PseudoHapticsR.CD = PseudoHapticsR.CD * 10;
            PseudoHapticsR.CD = Mathf.Ceil(PseudoHapticsR.CD) / 10;

           

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (PseudoHapticsR.CD < 1.4f)
            {
                PseudoHapticsR.CD = PseudoHapticsR.CD + 0.1f;
            }

            PseudoHapticsR.CD = PseudoHapticsR.CD * 10;
            PseudoHapticsR.CD = Mathf.Floor(PseudoHapticsR.CD) / 10;
            
        }
        //Change left hand CDratio
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (PseudoHapticsL.CD > 0.6f)
            {
                PseudoHapticsL.CD = PseudoHapticsL.CD - 0.1f;
            }

            PseudoHapticsL.CD = PseudoHapticsL.CD * 10;
            PseudoHapticsL.CD = Mathf.Ceil(PseudoHapticsL.CD) / 10;

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (PseudoHapticsL.CD < 1.4f)
            {
                PseudoHapticsL.CD = PseudoHapticsL.CD + 0.1f;
            }

            PseudoHapticsL.CD = PseudoHapticsL.CD * 10;
            PseudoHapticsL.CD = Mathf.Floor(PseudoHapticsL.CD) / 10;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCDRatio : MonoBehaviour
{
    // Start is called before the first frame update

    public PseudoHaptics PseudoHapticsR;
    public PseudoHaptics PseudoHapticsL;

    public Text targetText;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float CDR = PseudoHapticsR.CD;
        float CDL = PseudoHapticsL.CD;
        
        
        StringBuilder builder = new StringBuilder();
        builder.Append("CDratio");
        builder.Append(Environment.NewLine);
        builder.Append("ç∂éË = "+ CDL+"   âEéË = "+ CDR);
        targetText.text = builder.ToString();

    }
}

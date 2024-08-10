using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : BeatReciever
{
    public bool reciveInput=false;
    public int inputRegist;
    

    public void Update()
    {
        float axis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(axis) >= 0.5 && reciveInput)
        { 
            inputRegist = Mathf.RoundToInt(axis);
            reciveInput = false;
        }
    }

    public override void PreBeatAction()
    {
        inputRegist = 0;
        reciveInput = true;
    }
    public override void BeatAction()
    {
        reciveInput = false;
    }
}

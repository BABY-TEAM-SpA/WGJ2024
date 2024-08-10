using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public abstract class BeatReciever: MonoBehaviour
{
    public BeatType myBeatType;

    private void OnEnable()
    {
        BeatManager.OnBeat += OnBeatAction;
    }

    private void OnDisable()
    {
        BeatManager.OnBeat -= OnBeatAction;
    }

    private void OnBeatAction(BeatType type)
    {
        if (type == myBeatType)
        {
            BeatAction();
        }
    }

    public void BeatAction()
    {
        /// implement 
    }
}

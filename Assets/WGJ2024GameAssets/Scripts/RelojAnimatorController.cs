using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelojAnimatorController : BeatReciever
{
    
    [Range(0f,2f), SerializeField] private float clockInitialTime;
    [SerializeField] private LeanTweenType clockInitialAnimType;
    [Range(0.8f,1.3f), SerializeField] private float clockAnimScale;
    [SerializeField] private LeanTweenType clockAnimType;


    public void Start()
    {
        //StartClock();
    }

    public void StartClock()
    {
        LeanTween.rotateY(this.gameObject, 0f, clockInitialTime).setEase(clockInitialAnimType);
        LeanTween.scale(this.gameObject, Vector3.one, clockInitialTime).setEase(clockInitialAnimType)
            .setOnComplete(() => LevelController.Instance.StartGame());
    }

    public override void HalfBeatAction()
    {
        AnimateClock();
    }

    
    public void AnimateClock()
    {
        float animTime = (float)(BeatManager.Instance.bpmDuration/2 * 0.7f);
        this.transform.localScale = Vector3.one;
        LeanTween.cancel(this.gameObject);
        LeanTween.scale(this.gameObject,Vector3.one*clockAnimScale,animTime).setEase(clockAnimType).setLoopClamp(1);
    }
}

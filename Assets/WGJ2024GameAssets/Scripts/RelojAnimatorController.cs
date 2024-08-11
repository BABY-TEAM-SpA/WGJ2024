using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelojAnimatorController : BeatReciever
{

    [Range(0.8f,1.3f), SerializeField] private float clockAnimScale;
    [SerializeField] private LeanTweenType clockAnimType;


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

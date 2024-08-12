using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magocontroller : BeatReciever
{
    
    [SerializeField] private bool isAttacking;
    [SerializeField] private int currentAttackindex=0;
    [SerializeField] private AttackDirections currentAttackDirections;
    List<int> hurtPos = new List<int>();
    [SerializeField] private List<LineRenderer> lasers = new List<LineRenderer>();
    [SerializeField] private float maxWidth;
    [SerializeField] private LeanTweenType animCurveIn;
    [SerializeField] private LeanTweenType animCurveOut;
    
    
    
    
    public static Magocontroller Instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this.gameObject); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public override void PreBeatAction()
    {
        currentAttackindex += 1;
        GenerateAttack();
        base.PreBeatAction();
    }

    public override void BeatAction()
    {
       SpawnLasers(); 
    }
    

    public override void PostBeatAction()
    {

    }

    public void GenerateAttack()
    {
        if (currentAttackindex >= LevelController.Instance.attackSet.attacks.Count)
        {
            
        }
        else
        {
            hurtPos = new List<int>();
            currentAttackDirections = LevelController.Instance.attackSet.attacks[currentAttackindex];
            switch (currentAttackDirections)
            {
                case AttackDirections.Left:
                    hurtPos.Add(0);
                    break;
                case AttackDirections.Mid:
                    hurtPos.Add(1);
                    break;
                case AttackDirections.Right:
                    hurtPos.Add(2);
                    break;
                case AttackDirections.Left_Mid:
                    hurtPos.Add(0);
                    hurtPos.Add(1);
                    break;
                case AttackDirections.Right_Mid:
                    hurtPos.Add(1);
                    hurtPos.Add(2);
                    break;
                case AttackDirections.Left_Right:
                    hurtPos.Add(0);
                    hurtPos.Add(2);
                    break;
            } 
        }
        
    }

    public void SpawnLasers()
    {
        foreach (int laserValue in hurtPos)
        {
            lasers[laserValue].widthMultiplier = 0f;
            LeanTween.value(gameObject, (float x) => { lasers[laserValue].widthMultiplier = x; }, 0f, maxWidth,
                BeatManager.Instance.bpmDuration / 2 * 0.7f).setEase(animCurveIn).setOnComplete(() =>
            {
                LeanTween.value(gameObject, (float x) => { lasers[laserValue].widthMultiplier = x; }, maxWidth, 0f,
                    BeatManager.Instance.bpmDuration / 2 * 0.7f).setEase(animCurveOut);
                PlayerController.Instance.TryHurtPlayer(laserValue);
            });
        }
    }

    public void ResetMago()
    {
        currentAttackindex = -1;
        foreach (LineRenderer laser in lasers)
        {
            if(LeanTween.isTweening(laser.gameObject)) LeanTween.cancel(laser.gameObject);
            //laser.widthMultiplier = 0;
        }
    }
}

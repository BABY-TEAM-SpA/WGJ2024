using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magocontroller : BeatReciever
{
    
    [SerializeField] private bool isAttacking;
    [SerializeField] private int currentAttack=-1;
    
    
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
        currentAttack += 1;
        base.PreBeatAction();
    }

    public override void BeatAction()
    {
        AttackDirections attackDirections = LevelController.Instance.attackSet.attacks[currentAttack];
        //Encender Lasers
    }

    public override void PostBeatAction()
    {
        
        //Apagar Lasers
        
        //PlayerController.Instance.TryHurtPlayer();
    }

    public void ResetMago()
    {
        currentAttack = -1;
    }
}

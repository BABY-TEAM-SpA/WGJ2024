using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : BeatReciever
{
    public bool reciveInput=false;
    private bool inputDetectedBeforeCheck = false; 
    public int inputRegist;
    public int currentCarril=1;
    [SerializeField] private List<Transform> positions = new List<Transform>();
    [SerializeField] private GameObject playerSprite;


    [Header("Movement Animation")] 
    [SerializeField] private float moveAnimTime;
    [SerializeField] private LeanTweenType moveAnimType;

    public static PlayerController Instance { get; private set; }

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
    public void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (!reciveInput)
            {
                inputDetectedBeforeCheck = true;
            }
            else if (reciveInput && !inputDetectedBeforeCheck&& BeatManager.Instance.onMargen)
            {
                if(Input.GetAxis("Horizontal")>0)ClickRight();
                else ClickLeft();
                reciveInput = false;
            }
        }
        if (!reciveInput)
        {
            inputDetectedBeforeCheck = false;
        }
        
    }

    public override void PreBeatAction()
    {
        inputRegist = 0;
        reciveInput = true;
    }
    public override void BeatAction()
    {
        PlayerMoveForward();
    }

    public override void PostBeatAction()
    {
        PlayerMoveSide();
        inputRegist = 0;
        reciveInput = false;
    }

    public void PlayerMoveForward()
    {
        LeanTween.move(gameObject, this.transform.position+this.transform.forward*LevelController.Instance.stepDistance, moveAnimTime).setEase(moveAnimType);
    }

    public void PlayerMoveSide()
    {
        int newPosition = currentCarril + inputRegist;
        if (newPosition >= 0 && newPosition < positions.Count)
        {
            currentCarril = newPosition;
            LeanTween.move(playerSprite, positions[currentCarril].position, moveAnimTime).setEase(moveAnimType);
        }
        
    }

    public void ResetPlayer(Vector3 initialPos)
    {
        this.transform.position = initialPos;
        currentCarril = 1;
        playerSprite.transform.position = positions[currentCarril].position;
    }

    public void ClickLeft()
    {
        if (reciveInput && BeatManager.Instance.onMargen)
        {
            inputRegist = -1;
            reciveInput = false;
        }
    }

    public void ClickRight()
    {
        if (reciveInput && BeatManager.Instance.onMargen)
        {
            inputRegist = 1;
            reciveInput = false;
        }
    }

    public void TryHurtPlayer(int damagePos)
    {
        if (currentCarril == damagePos)
        {
            HurtPlayer();
        }
    }

    private void HurtPlayer()
    {
        //
        LevelController.Instance.StartGame();
    }
}

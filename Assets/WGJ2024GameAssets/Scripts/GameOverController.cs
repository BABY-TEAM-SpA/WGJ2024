using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public string sceneName;
    [SerializeField] private GameObject texto;
    [SerializeField] private float textoAnimTime;
    [SerializeField] private LeanTweenType textoAnimCurve;
    [SerializeField] private GameObject boton;
    [SerializeField] private float botonAnimTime;
    [SerializeField] private LeanTweenType botonAnimCurve;
    [SerializeField] private GameObject sello;
    [SerializeField] private float selloAnimTime;
    [SerializeField] private LeanTweenType selloAnimeCurve;
    
    void Start()
    {
        LeanTween.scale(texto, Vector3.one, textoAnimTime).setEase(textoAnimCurve).setOnComplete(() =>
        {
            LeanTween.scale(boton, Vector3.one, botonAnimTime).setEase(botonAnimCurve).setOnComplete(() =>
            {
                LeanTween.scale(sello, Vector3.one, selloAnimTime).setEase(selloAnimeCurve);
            });
        });
    }

    void LoadInitialScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Determine le score a attaindre pour la victoire
    public int scoreVictoire = 20;

    //Permet a un autre script d'acceder a ce script
    public static UIManager instance;

    //Determine l'ui du score
    [SerializeField] TextMeshProUGUI scoreText;

    public GameObject UiVictoire;

    public GameObject UiJeu;
 
    //Determine le score
    public int score = 0;

    public string scoreString;

    void Start()
    {
        //Desactive le canvas de victoire
        UiVictoire.SetActive(false);
        //Renitalise le scrore en debut de jeu
        score = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreString = score.ToString() + " POINTS";
        scoreText.text = scoreString;

        if (score >= scoreVictoire)
        {
            Victoire();
        }
    }
    private void Awake()
    {
        instance = this;
    }
    
    public void AddPoint()
    {
        score += 1;
        Debug.Log("+1");
    }

    void Victoire()
    {
        //Desactive le canvas de jeu
        UiJeu.SetActive(false);
        // Active le Canvas de defaite
        UiVictoire.SetActive(true);
        Debug.Log("Victoire !");
    }
}

    
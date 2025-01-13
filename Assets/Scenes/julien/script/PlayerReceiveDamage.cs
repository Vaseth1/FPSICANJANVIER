using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveDamage : MonoBehaviour
{
    //Maximum de points de vie
    public int maxHitPoint = 3;
    
    //Points de vie actuels
    public int hitPoint = 0;

    public GameObject vie3;
    public GameObject vie2;
    public GameObject vie1;
    public GameObject vie0;

    public GameObject UiDefaite;

    public GameObject UiJeu;

    // Start is called before the first frame update
    void Start()
    {
        //Desactive le canvas defaite
        //Active le canvas jeu
        UiDefaite.SetActive(false);
        UiJeu.SetActive(true);
        //Au debut : Points de vie actuels = Maximum de points de vie
        hitPoint = maxHitPoint;
    }

    public void AddDamage(int damage)
    {    
        //Applique les dommages aux points de vies actuels
        hitPoint -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoint == 3)
        {
            vie3.SetActive(true);
            vie2.SetActive(false);
            vie1.SetActive(false);
            vie0.SetActive(false);
        }

        if (hitPoint == 2)
        {
            vie3.SetActive(false);
            vie2.SetActive(true);
            vie1.SetActive(false);
            vie0.SetActive(false);
        }
        if (hitPoint == 1)
        {
            vie3.SetActive(false);
            vie2.SetActive(false);
            vie1.SetActive(true);
            vie0.SetActive(false);
        }
        if (hitPoint == 0)
        {
            vie3.SetActive(false);
            vie2.SetActive(false);
            vie1.SetActive(false);
            vie0.SetActive(true);
        }

        if (hitPoint == 0)
        {
            Defaite();
        }
    }
    
    void Defaite()
    {
        //Desactive le canvas de jeu
        UiJeu.SetActive(false);
        // Active le Canvas de defaite
        UiDefaite.SetActive(true);
        Debug.Log("Defaite !");
    }
}

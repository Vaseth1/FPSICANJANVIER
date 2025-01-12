using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ReceiveDamage : MonoBehaviour
{ 
    //Maximum de points de vie
    public int maxHitPoint = 5;
    
    //Points de vie actuels
    public int hitPoint = 0;
    
    //Apres avoir recu un degat :
    //L' ennemi est invulnerable quelques instants
    public bool isInvulnerable;
    
    //Temps d'invulnerabiliter
    public float invulnerabiltyTime;
    
    //Temps depuis le dernier degat
    private float timeSinceLastHit = 0.0f;

    //Compteur de scorring
    public int scoreValue = 0;

    //Determine le score a attaindre pour la victoire
    public int scoreVictoire = 2;

    //temps avant la mort
    private float TimeToDie = 3f;
    
    private void Start()
    {
        //Au debut : Points de vie actuels = Maximum de points de vie
        hitPoint = maxHitPoint;
        
        isInvulnerable = false;
    }
    
    private void Update()
    {
        //Si invulnerable
        if (isInvulnerable)
        {
            //Compte le temps depuis le dernier degat
            //timeSinceLastHit = temps depuis le dernier degat
            //Time.deltaTime = temps ecoule depuis la derniere frame
            timeSinceLastHit += Time.deltaTime;
            
            if (timeSinceLastHit > invulnerabiltyTime)
            {
                //Le temps est ecoule, il n'est plus invulnerable
                timeSinceLastHit = 0.0f;
                isInvulnerable = false;
            }
        }
    }

    //Permet de recevoir des dommages
    public void GetDamage(int damage)
    {
        if (isInvulnerable)
            return;
        
        isInvulnerable = true;
            
        //Applique les dommages aux points de vies actuels
        hitPoint -= damage;
            
        //S'il reste des points de vie
        if (hitPoint > 0)
        {
            //SendMessage appellera toutes les methodes "TakeDamage" de ce GameObject
            //Exemple : "TakeDamage" est dans MonsterController
            gameObject.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
        }
        //Sinon
        else
        {
            //SendMessage appellera toutes les methodes "Defeated" de ce GameObject
            //Exemple : "Defeated" est dans MonsterController
            gameObject.SendMessage("Defeated", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject, TimeToDie);
            scoreValue += 1;
//            if (scoreValue > scoreVictoire)
//            {
//
//            }
            
        }
    }
}
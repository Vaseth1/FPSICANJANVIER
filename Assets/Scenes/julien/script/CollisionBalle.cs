using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBalle : MonoBehaviour
{
    //Dommage que le Gun inflige
    public int gunDamage = 1;
    
    // Update is called once per frame
    void Update()
    {
        
        
        
        
    }   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            // L'impact a toucher l'objet avec le tag "VotreTag"
            Debug.Log("Impact avec le tag detecte !");
            // Verifie si la cible a un RigidBody attache
            if (collision.gameObject.GetComponent<Rigidbody>() != null)
            {
                //S'assure que la cible toucher a un composant ReceiveDamage
                if (collision.gameObject.GetComponent<ReceiveDamage>() != null)
                {
                    //Envoie les dommages a la cible
                    collision.gameObject.GetComponent<ReceiveDamage>().GetDamage(gunDamage);
                }
            }
        }
    }   
}
                        
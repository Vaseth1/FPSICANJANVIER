using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Balle;
    public Transform ZonneSpawn;
    public KeyCode ToucheAPresser;
    public float PuissanceTir = 1000;
    //Float : memorise le temps du prochain tir possible
    private float nextFire;
    //Determine sur quel Layer on peut tirer
    public LayerMask layerMask;
    //Dommage que le Gun inflige
    public int gunDamage = 1;
    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.25f;


    // Update is called once per frame
    void Update()
    {
        
            

        // Verifie si le joueur a presser le bouton pour faire feu (ex:bouton gauche souris)
        // Time.time > nextFire : verifie si suffisament de temps s'est ecouler pour pouvoir tirer a nouveau
        if (Input.GetKeyDown(ToucheAPresser) && Time.time > nextFire)
        {
            //Nouveau tir

            //Met a jour le temps pour le prochain tir
            //Time.time = Temps ecouler depuis le lancement du jeu
            //temps du prochain tir = temps total ecouler + temps qu'il faut attendre
            nextFire = Time.time + fireRate;

            print(nextFire);

            //Instancie la balle
            GameObject Feu = Instantiate(Balle, ZonneSpawn.position, ZonneSpawn.rotation);
            //ajoute une force * puissance
            Feu.GetComponent<Rigidbody>().AddForce(ZonneSpawn.forward * PuissanceTir);
            Destroy(Feu, 3f);        
        }
    }
}

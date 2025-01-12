using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{
    //Dommage que le Gun inflige
    public int gunDamage = 1;

    //Porter du tir
    public float weaponRange = 200f;

    //Force de l'impact du tir
    public float hitForce = 100f;

    //La camera
    private Camera fpsCam;

    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.25f;

    //Float : memorise le temps du prochain tir possible
    private float nextFire;

    //Determine sur quel Layer on peut tirer
    public LayerMask layerMask;

    //Determine l'objet a instancier
    public GameObject Projectile;

    //Determine le point de depart
    public GameObject PointDepart;

    // Start is called before the first frame update
    void Start()
    {

        //Reference de la camera. GetComponentInParent<Camera> permet de chercher une Camera
        //dans ce GameObject et dans ses parents.
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verifie si le joueur a presser le bouton pour faire feu (ex:bouton gauche souris)
        // Time.time > nextFire : verifie si suffisament de temps s'est ecouler pour pouvoir tirer a nouveau
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            //Nouveau tir

            //Met a jour le temps pour le prochain tir
            //Time.time = Temps ecouler depuis le lancement du jeu
            //temps du prochain tir = temps total ecouler + temps qu'il faut attendre
            nextFire = Time.time + fireRate;

            print(nextFire);

            //On va lancer un rayon invisible qui simulera les balles du gun

            //Creer un vecteur au centre de la vue de la camera
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            //RaycastHit : permet de savoir ce que le rayon a toucher
            RaycastHit hit;


            // Verifie si le raycast a toucher quelque chose
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
            {
                print("Target");

                // Verifie si la cible a un RigidBody attache
                if (hit.rigidbody != null)
                {

                    //AddForce = Ajoute Force = Pousse le RigidBody avec la force de l'impact
                    hit.rigidbody.AddForce(-hit.normal * hitForce);

                    //S'assure que la cible toucher a un composant ReceiveDamage
                    if (hit.collider.gameObject.GetComponent<ReceiveDamage>() != null)
                    {
                        //Envoie les dommages a la cible
                        hit.collider.gameObject.GetComponent<ReceiveDamage>().GetDamage(gunDamage);
                    }
                }
            }
        }
    }
}
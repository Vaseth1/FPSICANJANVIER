using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemi : MonoBehaviour
{
    //Determine l'ennemi a instancier
    public GameObject EnnemiPrefab;

    //Determine la position du spawn
    public Transform SpawnPoint;

    //Determine l'interval entre chaque spawn
    public float spawnInterval;

    //Determine jusque ou les ennemi spawn 
    public float distanceSpawn;

    //Determine la position du joueur
    public Transform Player;

    //Determine la distance du joueur par rapport au spawn
    public float Distance;    

    //Max spawn sur la map
    public int MaxSpawn = 2;
    
    //Determine si on peut respawn un ennemi
    public bool ReSpawn;


    //Determine le prochain spawn
    private float NextSpawn;

    //Decompte pour max spawn
    private int compte;

    //Determine le nombre d'ennemi sur la map
    private int NbTotal;

    // Update is called once per frame
    void Update()
    {
        //Determine la valeur entre le spawn et le joueur
        Distance = (SpawnPoint.position - Player.position).magnitude;
        //Debug.Log(Distance);
        
        if (Distance < distanceSpawn && compte < MaxSpawn)
        {
            if (Time.time > NextSpawn)
            {
                //Prochain spawn = temps depuis start + interval de spawn
                //Instancie l'ennemi
                //Renome le clonne
                //Ajoute +1 au compteur
                NextSpawn = Time.time + spawnInterval;
                GameObject ennemiSpawn = Instantiate(EnnemiPrefab, SpawnPoint.position, Quaternion.identity) as GameObject;
                ennemiSpawn.name = "E" + this.name;
                compte++;
            }
           
        }

        if (ReSpawn)
        {
            NbTotal = 0;
            //Boucle 
            foreach (GameObject Ennemi in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (Ennemi.name == "E" + this.name)
                {
                    NbTotal++;
                }
            }
            if (NbTotal < MaxSpawn)
            {
                compte = NbTotal;
            }
        }
    }
}

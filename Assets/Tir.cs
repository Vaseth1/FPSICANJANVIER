using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tir : MonoBehaviour
{
    public GameObject Projectil;

    public float hitForce = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input. GetMouseButtonDown(0))
        {
            LancerBalle();
        }
    }
    void LancerBalle()
    {
        //Instancie le projectil a la position de eject
        GameObject Projectile = Instantiate(Projectil, transform.position, transform.rotation);
        //Recupere le Rigidbody du projectil
        Rigidbody rb = Projectile.GetComponent<Rigidbody>();
        //Applique une force dans la direction de eject
        rb.AddForce(transform.forward * hitForce, ForceMode.Impulse);
        //Detruit le clone au bout de 3s
        Destroy(Projectile, 3f);
    }
}

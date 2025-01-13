using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MeleeWeapon : MonoBehaviour
{
    private PlayerReceiveDamage playerReceiveDamage;

    //Damage que fait l'arme
    public int damage = 1;
 
    //Determine quel Layer on peut toucher
    public LayerMask layerMask;
 
    //Est-ce que l'arme est en train d'etre utiliser ?
    public bool isAttacking = false;
 
    void Start()
    {
        playerReceiveDamage = FindObjectOfType<PlayerReceiveDamage>();
    }
 
    public void StartAttack()
    {
        isAttacking = true;
    }
 
    public void StopAttack()
    {
        isAttacking = false;
    }
 
    //Quand MeleeWeapon entre en collision avec objet
    private void OnTriggerEnter(Collider other)
    {
        if (!isAttacking)
            return;
 
        if ((layerMask.value & (1 << other.gameObject.layer)) == 0)
            return;
 
            //Fait des dommages au GameObject qu'on a toucher
            if (playerReceiveDamage != null)
            {
                playerReceiveDamage.AddDamage(damage);
            }
 
    }
}
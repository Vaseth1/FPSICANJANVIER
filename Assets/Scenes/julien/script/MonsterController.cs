using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
 
public class MonsterController : MonoBehaviour
{
    public GameObject player;
 
    public MeleeWeapon meleeWeapon;
 
    //Agent de Navigation
    NavMeshAgent navMeshAgent;
 
 
    //Composants
    Animator animator;
 
    //Actions possibles
 
    //Stand ou Idle = attendre
    const string STAND_STATE = "Stand";
 
    //Recoit des dommages
    const string TAKE_DAMAGE_STATE = "Damage";
 
    //Est vaincu
    public const string DEFEATED_STATE = "Defeated";
 
    //Est en train de marcher
    public const string WALK_STATE = "Walk";
 
    //Attaque
    public const string ATTACK_STATE = "Attack";
 
 
    //Memorise l'action actuelle
    public string currentAction;
 
    private void Awake()
    {
        //Au depart, la creature attend en restant debout
        currentAction = STAND_STATE;
 
        //Reference vers l'Animator
        animator = GetComponent<Animator>();
 
        //Reference NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
 
        //Reference de Player
        player = FindObjectOfType<PlayerFPS>().gameObject;
    }
 
    private void Update()
    {
 
        //si la creature est defaite
        //Elle ne peut rien faire d'autres
        if (currentAction == DEFEATED_STATE)
        {
            navMeshAgent.ResetPath();
            return;
        }
 
 
        //Si la creature reeoit des dommages:
        //Elle ne peut rien faire d'autres.
        //Cela servira quand on ameliorera ce script.
        if (currentAction == TAKE_DAMAGE_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }
 
        if (player != null)
        {
            //Est-ce que l'IA se deplace vers le joueur ?
            if (MovingToTarget())
            {
                //En train de marcher
                return;
            }
            else
            {
                if (currentAction != ATTACK_STATE && currentAction != TAKE_DAMAGE_STATE)
                {
                    Attack();
                    return;
                }
                if (currentAction == ATTACK_STATE)
                {
                    Attacking();
                    return;
                }
 
                //Defaut
                Stand();
                return;
            }
 
 
 
        }
    }
 
    //La creature attend
    private void Stand()
    {
        //Reinitialise les parametres de l'animator
        ResetAnimation();
        //L'action est maintenant "Stand"
        currentAction = STAND_STATE;
        //Le parametre "Stand" de l'animator = true
        animator.SetBool("Stand", true);
    }
 
    public void TakeDamage()
    {
        //Reinitialise les parametres de l'animator
        ResetAnimation();
        //L'action est maintenant "Damage"
        currentAction = TAKE_DAMAGE_STATE;
        //Le parametre "Damage" de l'animator = true
        animator.SetBool("Damage", true);
    }
 
    public void Defeated()
    {
        //Reinitialise les parametres de l'animator
        ResetAnimation();
        //L'action est maintenant "Defeated"  
        currentAction = DEFEATED_STATE;
        //Le parametre "Defeated" de l'animator = true
        animator.SetBool(DEFEATED_STATE, true);
    }
 
 
    //Permet de surveiller l'animation lorsque l'on prend un degat
    private void TakingDamage()
    {
 
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(TAKE_DAMAGE_STATE))
        {
            //Compte le temps de l'animation
            //normalizedTime : temps ecouler nomraliser (de 0 a 1).
            //Si normalizedTime = 0 => C'est le debut.
            //Si normalizedTime = 0.5 => C'est la moitier.
            //Si normalizedTime = 1 => C'est la fin.
 
 
            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
 
 
            //Fin de l'animation
            if (normalizedTime > 1)
            {
                Stand();
            }
 
        }
 
    }
 
    private void Attacking()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(ATTACK_STATE))
        {
            //Compte le temps de l'animation
            //normalizedTime : temps ecouler nomraliser (de 0 a 1).
            //Si normalizedTime = 0 => C'est le debut.
            //Si normalizedTime = 0.5 => C'est la moitier.
            //Si normalizedTime = 1 => C'est la fin.
 
 
 
 
            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
 
 
            //Fin de l'animation
            if (normalizedTime > 1)
            {
 
                meleeWeapon.StopAttack();
                Stand();
                return;
            }
 
            meleeWeapon.StartAttack();
 
        }
    }
 
 
    private bool MovingToTarget()
    {
 
        //Assigne la destination : le joueur
        navMeshAgent.SetDestination(player.transform.position);
 
        //Si navMeshAgent n'est pas pret
        if (navMeshAgent.remainingDistance == 0)
            return true;
 
 
        // navMeshAgent.remainingDistance = distance restante pour atteindre la cible (Player)
        // navMeshAgent.stoppingDistance = a quelle distance de la cible l'IA doit s'arreter 
        // (exemple 2 m pour le corps a sorps) 
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
 
            if (currentAction != WALK_STATE)
                Walk();
 
        }
        else
        {
            //Si arriver a bonne distance, regarde vers le joueur
            RotateToTarget(player.transform);
 
            return false;
        }
 
        return true;
    }
 
 
    //Walk = Marcher
    private void Walk()
    {
        //Reinitialise les parametres de l'animator
        ResetAnimation();
        //L'action est maintenant "Walk"
        currentAction = WALK_STATE;
        //Le parametre "Walk" de l'animator = true
        animator.SetBool(WALK_STATE, true);
    }
 
 
    private void Attack()
    {
        //Reinitialise les parametres de l'animator
        ResetAnimation();
        //L'action est maintenant "Attack"
        currentAction = ATTACK_STATE;
        //Le parametre "Attack" de l'animator = true
        animator.SetBool(ATTACK_STATE, true);
    }
 
    //Permet de tout le temps regarder en direction de la cible
    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }
 
    //Reinitialise les parametres de l'animator
    private void ResetAnimation()
    {
        animator.SetBool(STAND_STATE, false);
        animator.SetBool(TAKE_DAMAGE_STATE, false);
        animator.SetBool(DEFEATED_STATE, false);
        animator.SetBool(WALK_STATE, false);
        animator.SetBool(ATTACK_STATE, false);
    }
 
}
using UnityEngine;
using UnityEngine.UI;

public class Defaite : MonoBehaviour
{
    public int playerHealth = 10; // Points de vie initiaux du joueur
    public GameObject UiDefaite;

    void Start()
    {
        if (UiDefaite != null)
        {
            UiDefaite.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UiDefaite n'est pas assigné !");
        }
    }

    public void TakeDamage(int damage)
    {
        // Réduit les points de vie du joueur
        playerHealth -= damage;
        Debug.Log("Points de vie restants : " + playerHealth);

        // Vérifie si le joueur a atteint 0 points de vie
        if (playerHealth <= 0)
        {
            ShowDefeatScreen();
        }
    }

    void ShowDefeatScreen()
    {
        // Active le Canvas de défaite
        if (UiDefaite != null)
        {
            UiDefaite.SetActive(true);
            Debug.Log("Défaite !");
        }
    }
}

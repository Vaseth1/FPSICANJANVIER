using UnityEngine;
using UnityEngine.UI;

public class Victoire : MonoBehaviour
{
    public int playerScore = 0; // Score initial du joueur
    public GameObject UiVictoire;

    void Start()
    {
        
        if (UiVictoire != null)
        {
            UiVictoire.SetActive(false);
        }
        else
        {
            Debug.LogWarning("UiVictoire n'est pas assigné !");
        }
    }

    public void AddPoint()
    {
        // Ajoute 1 point au joueur
        playerScore++;
        Debug.Log("Score du joueur: " + playerScore);

        // Vérifie si le joueur a atteint 2 points
        if (playerScore == 2)
        {
            ShowVictoryScreen();
        }
    }

    void ShowVictoryScreen()
    {
        // Active le Canvas de victoire
        if (UiVictoire != null)
        {
            UiVictoire.SetActive(true);
            Debug.Log("Victoire !");
        }
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0; // Le score du joueur

    // M�thode pour augmenter le score
    public void IncreaseScore(int value)
    {
        score += value;
        Debug.Log("Score : " + score); // Affiche le score dans la console pour la v�rification
    }
}

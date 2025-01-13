using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        // Recharge la scene actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeckMakeButton : MonoBehaviour {
    public void Connect()
    {
        SceneManager.LoadScene("Deck");
    }
    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
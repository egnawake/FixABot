using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Quit"))
        {
            Quit();
        }
    }
}

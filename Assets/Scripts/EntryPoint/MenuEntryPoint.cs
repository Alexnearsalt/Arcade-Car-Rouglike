using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;
    
    
    private IEnumerator Start()
    {
        StartCoroutine(BindObjects());

        yield return null;
    }

    private IEnumerator BindObjects()
    {
        eventSystem = Instantiate(eventSystem);
        music = Instantiate(music);
        settingsMenu = Instantiate(settingsMenu);
        mainMenu = Instantiate(mainMenu);
        yield return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapEntryPoint : MonoBehaviour
{
    [SerializeField] private VolumeBootstrap volumeBootstrap;
    [SerializeField] private ResolutionBootstrap resolutionBootstrap;
    private IEnumerator Start()
    {
        StartCoroutine(BindObjects());
        //Localisation
        //Объекты работающие на все приложение
        Debug.Log("Load complete");
        SceneManager.LoadScene("Menu Scene");
        yield break;
    }

    private IEnumerator BindObjects()
    {
        volumeBootstrap = Instantiate(volumeBootstrap);
        resolutionBootstrap = Instantiate(resolutionBootstrap);
        yield return null;
    }
}

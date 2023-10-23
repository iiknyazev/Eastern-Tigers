using UnityEngine;

public class MyPlayerPrefs : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("IsTutorial",  1);
        DontDestroyOnLoad(gameObject);
    }
}

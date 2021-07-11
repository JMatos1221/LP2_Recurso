using LP2_Recurso;
using UnityEditor;
using UnityEngine;

public class Controller : MonoBehaviour, IController
{
    public void TogglePause()
    {
        
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

#if UNITY_STANDALONE
        Application.Quit(0);
#endif
    }
}
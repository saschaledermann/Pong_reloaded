using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("Main");
    }
}

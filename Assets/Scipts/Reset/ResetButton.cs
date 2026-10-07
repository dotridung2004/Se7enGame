using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private Button btnReset;
    void Start()
    {
        btnReset.onClick.AddListener(ResetScene);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        btnReset.onClick.RemoveListener(ResetScene);
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

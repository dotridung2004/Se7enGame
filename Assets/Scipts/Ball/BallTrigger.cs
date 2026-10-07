using UnityEngine;
using UnityEngine.UI;

public class BallTrigger : MonoBehaviour
{
    [SerializeField] private Button btnKick;
    [SerializeField] private BallKick ball;
    
    void Start()
    {
        btnKick.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            btnKick.gameObject.SetActive(true);
            btnKick.onClick.AddListener(ball.Kick);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            btnKick.gameObject.SetActive(false);
            btnKick.onClick.RemoveListener(ball.Kick);
        }
    }
}

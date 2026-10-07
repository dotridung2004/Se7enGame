using UnityEngine;
using UnityEngine.UI;

public class BallTrigger : MonoBehaviour
{
    [SerializeField] private Button btnKick;
    [SerializeField] private BallKick ball;
    
    private bool playerInRange;
    void Start()
    {
        btnKick.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(!playerInRange) return;
        RefreshButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        btnKick.onClick.RemoveListener(ball.Kick);
        btnKick.onClick.AddListener(ball.Kick);
        RefreshButton();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        btnKick.onClick.RemoveListener(ball.Kick);
        btnKick.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        btnKick.onClick.RemoveListener(ball.Kick);
    }

    private void RefreshButton()
    {
        btnKick.gameObject.SetActive(ball.CanKick);
    }
}

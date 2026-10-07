using UnityEngine;
using UnityEngine.UI;

public class AutoKickButton : MonoBehaviour
{
    [SerializeField] private Button btnAutoKick;
    [SerializeField] private Transform player;
    void Start()
    {
        btnAutoKick.onClick.AddListener(AutoKick);
    }

    private void OnDestroy()
    {
        btnAutoKick.onClick.RemoveListener(AutoKick);
    }

    private void AutoKick()
    {
        BallKick farthestBall = GetFarthestBall();
        if (farthestBall == null)
        {
            return;
        }
        farthestBall.Kick();
    }
    private BallKick GetFarthestBall()
    {
        BallKick[] balls = FindObjectsByType<BallKick>(FindObjectsSortMode.None);
        BallKick farhest = null;
        float maxSqrDistance = 0f;
        foreach (BallKick ball in balls)
        {
            float sqrDistance = (ball.transform.position - player.position).sqrMagnitude;
            if (sqrDistance > maxSqrDistance)
            {
                maxSqrDistance = sqrDistance;
                farhest = ball;
            }
        }
        return farhest;
    }
}

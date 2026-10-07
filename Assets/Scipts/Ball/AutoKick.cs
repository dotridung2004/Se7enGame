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

    private void Update()
    {
        if(AllBallsScored())
        {
            btnAutoKick.gameObject.SetActive(false);
            return;
        }
        btnAutoKick.gameObject.SetActive(true);
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

    private bool AllBallsScored()
    {
        BallKick[] balls = FindObjectsByType<BallKick>(FindObjectsSortMode.None);
        if (balls.Length == 0)
        {
            return false;
        }
        foreach(BallKick ball in balls)
        {
            if (!ball.hasScored)
            {
                return false;
            }
        }
        return true;
    }

    private BallKick GetFarthestBall()
    {
        BallKick[] balls = FindObjectsByType<BallKick>(FindObjectsSortMode.None);
        BallKick farhest = null;
        float maxSqrDistance = 0f;
        foreach (BallKick ball in balls)
        {
            if (!ball.CanKick)
            {
                continue;
            }
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

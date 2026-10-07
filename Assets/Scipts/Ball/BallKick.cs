using UnityEngine;

public class BallKick : MonoBehaviour
{
    [SerializeField] private Transform[] goalTarget;
    [SerializeField] private float ballSpeed = 60f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float cameraReturnDelay = 2f;
    private bool isFlying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void Kick()
    {
        Transform nearestGoal = GetNearestGoal();
        if (nearestGoal == null)
        {
            return;
        }
        Vector3 direction = (nearestGoal.position - transform.position).normalized;
        direction.y = 0f;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Vector3 velocity = direction * ballSpeed;
        rb.linearVelocity = velocity;
        isFlying = true;
        CameraFollowController.Instance.FollowBall(transform);
        StopAllCoroutines();

    }

    public bool OnReachedGoal()
    {
        if (!isFlying) return false;

        isFlying = false;
        StopAllCoroutines();
        CameraFollowController.Instance.ReturnToPlayer(cameraReturnDelay);
        return true;
    }

    private Transform GetNearestGoal()
    {
        Transform nearest = null;
        float minSqrDistance = float.MaxValue;
        foreach (Transform goal in goalTarget)
        {
            float sqrDistance = (goal.position - transform.position).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                nearest = goal;
            }
        }
        return nearest;
    }
}

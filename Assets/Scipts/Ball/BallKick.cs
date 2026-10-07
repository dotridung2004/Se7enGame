using System.Collections;
using UnityEngine;

public class BallKick : MonoBehaviour
{
    [SerializeField] private Transform[] goalTarget;
    [SerializeField] private float ballSpeed = 60f;
    [SerializeField] private float upwardSpeed = 2f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float cameraReturnDelay = 2f;

    [SerializeField] private float stopThreshold = 0.2f; 
    [SerializeField] private float maxFlyTime = 8f;       
    [SerializeField] private float settleDelayAfterGoal = 1f;
    private bool isFlying;
    public bool hasScored;
    public bool CanKick => !isFlying && !hasScored;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void Kick()
    {
        if(!CanKick) return;
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
        velocity.y = upwardSpeed;
        rb.linearVelocity = velocity;
        isFlying = true;
        CameraFollowController.Instance.FollowBall(transform);
        StopAllCoroutines();
        StartCoroutine(WatchBallStop());
    }

    public bool OnReachedGoal()
    {
        if (!isFlying) return false;

        isFlying = false;
        hasScored = true;
        StopAllCoroutines();
        CameraFollowController.Instance.ReturnToPlayer(cameraReturnDelay);
        StartCoroutine(SettleAfterGoal());
        return true;
    }

    private IEnumerator WatchBallStop()
    {
        yield return new WaitForSeconds(0.3f);

        float elapsed = 0f;

        while (rb.linearVelocity.magnitude > stopThreshold && elapsed < maxFlyTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        FreezeBall();
        isFlying = false;
        CameraFollowController.Instance.ReturnToPlayer(cameraReturnDelay);
    }
    private IEnumerator SettleAfterGoal()
    {
        yield return new WaitForSeconds(settleDelayAfterGoal);
        FreezeBall();
    }
    private void FreezeBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
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

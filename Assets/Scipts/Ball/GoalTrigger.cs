using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject confettiEffectPrefab;
    //[SerializeField] private Transform confettiPoint;
    [SerializeField] private float destroyDelay = 2f;
    private void OnTriggerEnter(Collider other)
    {
        BallKick ballKick = other.GetComponent<BallKick>();
        if (ballKick == null) return;
        if (ballKick.OnReachedGoal())
        {
            //Debug.Log("Goal!");
            PlayConfetti();
        }
    }

    private void PlayConfetti()
    {
        if (confettiEffectPrefab == null) return;
        //Vector3 pos = confettiPoint != null ? confettiPoint.position : transform.position;
        GameObject fx = Instantiate(confettiEffectPrefab, transform.position, Quaternion.identity);
        Destroy(fx, destroyDelay);
    }
}

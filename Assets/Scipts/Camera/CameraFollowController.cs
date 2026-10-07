using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public static CameraFollowController Instance { get; private set; }

    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private Transform player;

    private Coroutine returnRoutine;

    void Awake()
    {
        Instance = this;
    }

    public void FollowBall(Transform ball)
    {
        CancelReturn();
        cinemachineCamera.Target.TrackingTarget = ball;
    }

    public void ReturnToPlayer(float delay)
    {
        CancelReturn();
        returnRoutine = StartCoroutine(ReturnRoutine(delay));
    }

    private IEnumerator ReturnRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        cinemachineCamera.Target.TrackingTarget = player;
        returnRoutine = null;
    }

    private void CancelReturn()
    {
        if (returnRoutine != null)
        {
            StopCoroutine(returnRoutine);
            returnRoutine = null;
        }
    }
}
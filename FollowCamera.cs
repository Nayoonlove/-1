using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 플레이어 캐릭터
    public Vector3 offset = new Vector3(0, 5, -5); // 카메라 위치 오프셋
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position); // 항상 캐릭터 바라보기
    }
}
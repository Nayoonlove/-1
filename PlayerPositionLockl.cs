using UnityEngine;

public class PlayerPositionLock : MonoBehaviour
{
    // 고정하고 싶은 위치
    public Vector3 fixedPosition;

    void Update()
    {
        transform.position = fixedPosition;
    }
}
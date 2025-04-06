using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ) * speed * Time.deltaTime;
        
        // 🔥 CharacterController를 사용하여 이동
        controller.Move(move);
    }
}
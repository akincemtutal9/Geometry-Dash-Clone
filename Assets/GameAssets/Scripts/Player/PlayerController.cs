using UniRx;
using UnityEngine;

namespace GameAssets.Scripts.Player
{
    public enum MoveSpeed{Slow = 0 , Fast = 1 , VeryFast = 2};
    public class PlayerController : MonoBehaviour
    {
        private readonly float[] speedValues = {8.6f,12.96f,19,27f};
        [Header("Enum")]
        [SerializeField] private MoveSpeed currentMoveSpeed;
        
        [Header("References")]
        [SerializeField] private Transform groundCheckTransform;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform playerSprite;
        
        [Header("Settings")]
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float groundCheckRadius;
        
        private Rigidbody2D rb;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            HandleMovement();
            HandleJumpInput();
        }

        private void HandleMovement()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    var moveSpeedIndex = (int)currentMoveSpeed;
                    transform.position += Vector3.right * speedValues[moveSpeedIndex] * Time.deltaTime;
                }).AddTo(this);
        }
        private void HandleJumpInput()
        {
            Observable.EveryFixedUpdate()
                .Subscribe(_ =>
                {
                    if (OnGrounded())
                    {
                        var rotation = playerSprite.rotation.eulerAngles;
                        rotation.z = Mathf.Round(rotation.z / 90) * 90;
                        playerSprite.rotation = Quaternion.Euler(rotation); // Complete the rotation
                        if (Input.GetMouseButton(0))
                        {
                            Jump();
                        }
                    }
                    else
                    {
                        playerSprite.Rotate(Vector3.back * rotationSpeed);
                    }
                })
                .AddTo(this);
        }
        private void Jump()
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
        }
        private bool OnGrounded()
        {
            return Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundMask);
        }
    }
}
using GameAssets.Scripts.Utils;
using UniRx;
using UnityEngine;

namespace GameAssets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Enum")]
        [SerializeField] private MoveSpeed currentMoveSpeed;
        [SerializeField] private GameModes currentGameMode;
        
        [Header("References")]
        [SerializeField] private float[] speedValues = {};
        [SerializeField] private Transform groundCheckTransform;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform playerSprite;
        
        [Header("Settings")]
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float groundCheckRadius;
        
        private Rigidbody2D rb;
        private int _gravity = 1;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            HandleMovement();
            LimitFallSpeed();
            HandleGameModeBehaviour();
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
        private void HandleGameModeBehaviour()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                Invoke(currentGameMode.ToString(), 0);
            }).AddTo(gameObject);
        }
        private void Cube()
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
                        playerSprite.Rotate(Vector3.back ,_gravity * rotationSpeed * Time.deltaTime);
                    }
                })
                .AddTo(this);
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
                        playerSprite.Rotate(Vector3.back , rotationSpeed * _gravity * Time.deltaTime );
                    }
                })
                .AddTo(this);
        }
        private void Jump()
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce * _gravity, ForceMode2D.Impulse);
        }
        private bool OnGrounded()
        {
            return Physics2D.OverlapBox(groundCheckTransform.position+Vector3.up- Vector3.up*(_gravity -1 / -2), Vector2.right * 1.1f + Vector2.up * groundCheckRadius,0,groundMask);
        }
        private bool TouhcWall()
        {
            return Physics2D.OverlapBox((Vector2)transform.position + (Vector2.right * 0.55f),
                Vector2.up * 0.8f + (Vector2.right * groundCheckRadius), 0, groundMask);
        }

        private void LimitFallSpeed()
        {
            Observable.EveryFixedUpdate().Subscribe(_ =>
            {
                if (rb.velocity.y < -24f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -24f);
                }
            }).AddTo(gameObject);
        }

        public void ChangeThroughPortal(MoveSpeed moveSpeed, GameModes gameMode , Gravity gravity, int state)
        {
            switch (state)
            {
                case 0:
                    currentMoveSpeed = moveSpeed;
                    break;
                case 1:
                    currentGameMode = gameMode;
                    break;
                case 2:
                    _gravity = (int)gravity;
                    rb.gravityScale = Mathf.Abs(rb.gravityScale) * (int)gravity;
                    break;
            }
        }
    }
}
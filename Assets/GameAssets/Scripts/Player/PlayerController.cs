using GameAssets.Scripts.Utils;
using UnityEngine;

namespace GameAssets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Enum")] [SerializeField] private MoveSpeed currentMoveSpeed;
        [SerializeField] private GameModes currentGameMode;

        [Header("References")] 
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform playerSprite;

        [Header("Settings")] 
        [SerializeField] private float[] speedValues = { };
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private float shipGravityScale;
        [SerializeField] private float cubeGravityScale;

        private Rigidbody2D rb;
        private int _gravity = 1;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleMovement();
            LimitFallSpeed();
            HandleOnWallHit();
            if (currentGameMode == GameModes.Cube)
            {
                Cube();
            }
            else if (currentGameMode == GameModes.Ship)
            {
                Ship();
            }
            //HandleGameModeBehaviour();
        }

        private void HandleMovement()
        {
            var moveSpeedIndex = (int)currentMoveSpeed; 
            transform.position += Vector3.right * (speedValues[moveSpeedIndex] * Time.deltaTime);
        }

        /**
        private void HandleGameModeBehaviour()
        {
             Invoke(currentGameMode.ToString(), 0); 
        }
        */
        private void HandleOnWallHit()
        {
            if (TouchWall())
            {
                SceneLoader.ReloadScene();
            }
        }
        
        private void Cube()
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
                playerSprite.Rotate(Vector3.back, _gravity * rotationSpeed * Time.deltaTime);
            }

            rb.gravityScale = cubeGravityScale * _gravity;
        }
        
        private void Ship()
        { 
            playerSprite.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2);

            if (Input.GetMouseButton(0))
                rb.gravityScale = -shipGravityScale;
            else
                rb.gravityScale = shipGravityScale;
            rb.gravityScale *= _gravity;
        }
        
        private void Jump()
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce * _gravity, ForceMode2D.Impulse);
        }
        private bool OnGrounded()
        {
            Vector3 downOffset = Vector3.down * _gravity * 0.5f;
            Vector2 rightSize = Vector2.right * 1.1f;
            Vector2 upSize = Vector2.up * groundCheckRadius;

            Vector3 position = transform.position + downOffset;
            Vector2 size = rightSize + upSize;

            return Physics2D.OverlapBox(position, size, 0, groundMask);
        }
        private bool TouchWall()
        {
            Vector2 offset = Vector2.right * 0.55f;
            Vector2 size = new Vector2(groundCheckRadius * 2, 0.8f);
            Vector2 position = (Vector2)transform.position + offset;

            return Physics2D.OverlapBox(position, size, 0, groundMask);
        }

        private void LimitFallSpeed()
        {
            if ((rb.velocity.y  * _gravity) < -24.2f)
            {
                rb.velocity = new Vector2(rb.velocity.x, -24.2f * _gravity);
            }
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
        
        private void OnDrawGizmosSelected()
        {
            DrawGizmoForOnGrounded();
            DrawGizmoForTouchWall();
        }

        private void DrawGizmoForOnGrounded()
        {
            Vector3 downOffset = Vector3.down * _gravity * 0.5f;
            Vector2 rightSize = Vector2.right * 1.1f;
            Vector2 upSize = Vector2.up * groundCheckRadius;

            Vector3 position = transform.position + downOffset;
            Vector2 size = rightSize + upSize;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(position, new Vector3(size.x, size.y, 0.1f));
        }

        private void DrawGizmoForTouchWall()
        {
            Vector2 offset = Vector2.right * 0.55f;
            Vector2 size = new Vector2(groundCheckRadius * 2, 0.8f);
            Vector2 position = (Vector2)transform.position + offset;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(position, new Vector3(size.x, size.y, 0.1f));
        }
        
    }
}
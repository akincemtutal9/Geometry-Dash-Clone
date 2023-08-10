using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GameAssets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const string Ground = nameof(Ground);
        
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float rotationDuration = 1f;

        private Rigidbody2D rb;
        
        private bool isGrounded = true;
        private bool rotateClockwise = true;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            HandleJumpInput();
            HandleGroundCollisionEnter();
            HandleGroundCollisionExit();
        }
        private void HandleJumpInput()
        {
            Observable.EveryFixedUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Space) && isGrounded) 
                .Subscribe(_ => JumpAndRotateObject())
                .AddTo(this);
        }
        private void JumpAndRotateObject()
        {
            RotateObject();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        private void RotateObject()
        {
            float rotationAngle = rotateClockwise ? 180f : -180f;
            transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + rotationAngle), rotationDuration)
                .OnComplete(() =>
                {
                    rotateClockwise = !rotateClockwise;
                });
        }
        private void HandleGroundCollisionEnter()
        {
            this.OnCollisionEnter2DAsObservable()
                .Where(collision => collision.gameObject.CompareTag(Ground))
                .Subscribe(_ => isGrounded = true)
                .AddTo(this);
        }
        private void HandleGroundCollisionExit()
        {
            this.OnCollisionExit2DAsObservable()
                .Where(collision => collision.gameObject.CompareTag(Ground))
                .Subscribe(_ => isGrounded = false)
                .AddTo(this);
        }
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Thông số di chuyển")]
    public float moveSpeed = 5f;
    public float turnSpeed = 720f;
    public float jumpForce = 5f;

    [Header("Tương tác")]
    public float interactRange = 2f;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("LỖI: Không tìm thấy Animator trong nhân vật con!");
        }
    }

    void Update()
    {
        isGrounded = Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRange);
            foreach (var hitCollider in hitColliders)
            {
                Interactable interactable = hitCollider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    break;
                }
            }
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ).normalized;

        Vector3 moveVelocity = movement * moveSpeed;
        moveVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = moveVelocity;

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.fixedDeltaTime);
        }

        if (animator != null)
        {
            float currentSpeed = movement.magnitude;

            if (currentSpeed > 0f)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.SetFloat("Speed", 1.0f);
                }
                else
                {
                    animator.SetFloat("Speed", 0.3f);
                }
            }
            else
            {
                animator.SetFloat("Speed", 0.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Package"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.currentMoney += 500;
            GameManager.Instance.UpdateUI();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI timeUpText;
    [SerializeField] private GameObject collectible;

    private int count;

    [SerializeField] private AudioClip pickSound;
    private AudioSource audioSource;

    [SerializeField] private TextMeshProUGUI timerText;
    private float startingTimer = 30f;
    private float currentTimer;

    [SerializeField] private Transform cam;

    private CharacterController controller;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector2 movement;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        count = 0;

        SetCountText();
        timeUpText.enabled = false;

        audioSource = GetComponent<AudioSource>();

        currentTimer = startingTimer;
    }

    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

            animator.SetFloat("Speed", 1);
        } else
        {
            animator.SetFloat("Speed", 0);
        }

        currentTimer -= 1 * Time.deltaTime;
        timerText.text = "Remaining Time: " + currentTimer.ToString("0");
        if (currentTimer <= 0f)
        {
            currentTimer = 0f;
            timeUpText.enabled = true;
        }

        if (currentTimer == 0f)
        {
            Destroy(GameObject.FindWithTag("PickUp"));
        }
    }

    void SetCountText()
    {
        countText.text = "Objects Collected: " + count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            count += 1;

            speed += 0.5f;

            SetCountText();

            if (currentTimer > 0f)
            {
                var position = new Vector3(Random.Range(-8, 8), 0.5f, Random.Range(-8, 8));
                Instantiate(collectible, position, Quaternion.identity);
            }

            audioSource.PlayOneShot(pickSound, 1F);
        }
    }
}

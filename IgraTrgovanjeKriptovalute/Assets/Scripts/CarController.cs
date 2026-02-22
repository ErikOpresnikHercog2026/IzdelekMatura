using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private GeneralManagerField GeneralManagerFieldLink;

    private PlayerInput controls;

    private Vector2 premik;

    private float HitrostPremikanja = 5f;

    public Rigidbody2D Rb;

    public Animator animator;

    private void Awake()
    {
        controls = new PlayerInput();
        GeneralManagerFieldLink = FindObjectOfType<GeneralManagerField>();

        controls.Gameplay.Movement.performed += ctx => premik = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => premik = Vector2.zero;

        controls.Gameplay.EnterHouse.performed += _ => GeneralManagerFieldLink.HwasPressed();


    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("horizontal", premik.x);
        animator.SetFloat("vertical", premik.y);
        animator.SetFloat("Speed", premik.sqrMagnitude);
    }
    void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + premik * HitrostPremikanja * Time.fixedDeltaTime);
    }
}

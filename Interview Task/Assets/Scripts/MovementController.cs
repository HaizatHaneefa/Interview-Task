using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController _controller;
    Animator anim;

    [SerializeField] private GameObject player;

    public float moveSpeed = 2f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = SimpleInput.GetAxisRaw("Horizontal");
        float vertical = SimpleInput.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        _controller.Move(movement * Time.deltaTime * moveSpeed); // handles movement

        if (movement != Vector3.zero)
        {
           _controller.gameObject.transform.rotation = Quaternion.LookRotation(movement); // handles rotation
        }

        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("et", true);
        }
        if (horizontal == 0 && vertical == 0)
        {
            anim.SetBool("et", false);
        }
    }
}

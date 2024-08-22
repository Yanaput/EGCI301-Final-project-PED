using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController player;
    public float movementSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Animator animator;
    PlayerInteraction playerInteraction;

    
    void Start(){
        animator = GetComponent<Animator>();
        player = GetComponent<CharacterController>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    void Update(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        HandleAnimation(horizontal, vertical);
        Interact();
    }

    void HandleAnimation(float horizontal, float vertical) {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude > 0f){
            animator.SetBool("isMoving", true);
            float targetDirection = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float turnAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetDirection, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, turnAngle, 0f);
            player.Move(direction * movementSpeed * Time.deltaTime);

            if(vertical > 0){
                if(horizontal < 0){
                    animator.SetBool("turnLeft", true);
                    animator.SetBool("turnRight", false);
                }
                else if(horizontal > 0){
                    
                    animator.SetBool("turnLeft", false);
                    animator.SetBool("turnRight", true);
                }
            }
            else if(vertical < 0){
                if(horizontal < 0){ 
                    animator.SetBool("turnLeft", false);
                    animator.SetBool("turnRight", true);
                }
                else if(horizontal > 0){
                    animator.SetBool("turnLeft", true);
                    animator.SetBool("turnRight", false);
                }
            }
            else{
                animator.SetBool("turnLeft", false);
                animator.SetBool("turnRight", false);
            }
            
            return;
        }
        animator.SetBool("isMoving", false);
        animator.SetBool("turnLeft", false);
        animator.SetBool("turnRight", false);
    }

    public void Interact(){
        //tool interact
        if(Input.GetButtonDown("Fire1")){
            playerInteraction.Interact();
        }
        //item interact
        if(Input.GetButtonDown("Fire2")){
            playerInteraction.ItemInteract();
        }
        if(Input.GetButtonDown("Fire3")){
            playerInteraction.ItemKeep();
        }
    } 
}


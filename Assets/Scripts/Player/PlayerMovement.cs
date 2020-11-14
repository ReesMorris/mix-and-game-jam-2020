using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


  public float speed = 10f;
  public Animator animator;

  private Rigidbody rigidbody;
  private string moveXAxis = "Horizontal";
  private string moveYAxis = "Vertical";
  private SpriteRenderer spriteRenderer;
  private bool canMove = true;

  void Start() {
    rigidbody = GetComponent<Rigidbody>();
    rigidbody.centerOfMass = Vector3.zero;
    rigidbody.inertiaTensorRotation = Quaternion.identity;
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void FixedUpdate() {
    if (canMove) {
      float inputX = Input.GetAxisRaw(moveXAxis);
      float inputY = Input.GetAxisRaw(moveYAxis);

      RunForces(inputX, inputY);
    } else {
      RunForces(0, 0);
    }
  }

  public void CanMove(bool move) {
    canMove = move;
  }

  void RunForces(float inputX, float inputY) {
    MoveX(inputX);
    MoveY(inputY);

    if (Mathf.Abs(inputX) != 0 || Mathf.Abs(inputY) != 0) animator.SetFloat("Speed", 1);
    else animator.SetFloat("Speed", 0);
  }

  void MoveX(float input) {
    rigidbody.AddForce(new Vector3(1, 0, 0) * speed * input, ForceMode.Force);
    if (!spriteRenderer.flipX && input < 0) spriteRenderer.flipX = true;
    if (spriteRenderer.flipX && input > 0) spriteRenderer.flipX = false;

  }

  void MoveY(float input) {
    rigidbody.AddForce(new Vector3(0, 1, 0) * speed * input, ForceMode.Force);
  }
}
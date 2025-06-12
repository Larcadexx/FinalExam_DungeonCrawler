using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit; 
    public float ySpeed = 1.0f;
    public float xSpeed = 1.5f;
    protected Animator anim;

    // Variabel untuk menyimpan input asli sebelum normalisasi
    protected Vector3 originalInput;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        
        // Simpan input asli sebelum normalisasi
        originalInput = input;
        
        // Normalisasi input untuk gerak diagonal
        if (input.magnitude > 1)
            input.Normalize();

        // Hitung moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // Tambah dorongan jika ada
        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Gerak secara vertikal
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);

        // Gerak secara horizontal
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);

        // Update animator dengan input asli (sebelum normalisasi)
        if (anim != null)
        {
            // Gunakan originalInput untuk parameter animator
            anim.SetFloat("MoveX", originalInput.x);
            anim.SetFloat("MoveY", originalInput.y);
            bool isMoving = originalInput.magnitude > 0.01f;
            anim.SetBool("IsMoving", isMoving);
            
            // Debug untuk melihat nilai parameter
            Debug.Log($"Input: X={originalInput.x:F2}, Y={originalInput.y:F2}, Magnitude={originalInput.magnitude:F2}");
            Debug.Log($"IsMoving set to: {isMoving}");
            Debug.Log($"Animator IsMoving: {anim.GetBool("IsMoving")}");
        }
    }
}
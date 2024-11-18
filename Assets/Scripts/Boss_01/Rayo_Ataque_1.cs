using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo_Ataque_1 : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public int damage;
    [SerializeField] private float raySpeed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * raySpeed * Vector2.right);
    }

}

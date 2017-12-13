using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    Animator anim;
    public float speed;
    private StateHandler state;
    public EnemyEntity boss;
	void Start ()
    {
        state = (StateHandler)GameObject.FindWithTag("State Machine").GetComponent(typeof(StateHandler));
        transform.position = state.PartyPosition;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movementVector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", movementVector.x);
            anim.SetFloat("input_y", movementVector.y);
        } else
        {
            anim.SetBool("isWalking", false);
        }

        rb.MovePosition(rb.position + movementVector * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BossCollider"))
        {
            state.ClearEnemies();
            state.AddEnemy(boss);
            state.ChangeState(GameState.Battle);
        }
    }
}

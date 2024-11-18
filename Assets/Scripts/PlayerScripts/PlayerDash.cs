using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashPower;

    Rigidbody2D rb;

    PlayerWater waterScript;
    SpriteRenderer sprite;

    private InputAction dashAction;

    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            dashAction = playerInput.actions["Dash"];
        }
    }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        waterScript = GetComponent<PlayerWater>();
    }

   /* private IEnumerator SpendWaterEffect()
    {
        sprite.color = Color.blue;
        yield return new WaitForSeconds(0.01f);
        sprite.color = Color.white;
    }*/

    public void Dash(InputAction.CallbackContext context)
    {
        if (waterScript.TotalWater() >= 1)
        {
        Debug.Log("Dashed");
            waterScript.TakeWater(1);
            rb.AddForce(transform.up * dashPower, ForceMode2D.Impulse);
         //   StartCoroutine(SpendWaterEffect());
        }
    }

    private void OnEnable()
    {
        if (dashAction != null)
        {
            dashAction.performed += Dash;
            dashAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (dashAction != null)
        {
            dashAction.performed -= Dash;
            dashAction.Disable();
        }
    }

}

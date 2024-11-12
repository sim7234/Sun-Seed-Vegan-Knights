using Unity.VisualScripting;
using UnityEngine;

public class PlayerMeleeCombatPrototype : MonoBehaviour
{

    bool inputR1 = false;
    bool doesDamage = false;
    Animator animator;
    public int swordDamage;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            inputR1 = true;
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            inputR1 = false;
        }

        if (inputR1 == true)
        {
            animator.SetTrigger("PressedR1");
            doesDamage = true;
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                doesDamage = false;
            }
        }

        
    }
    
}

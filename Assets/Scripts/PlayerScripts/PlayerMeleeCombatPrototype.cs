using UnityEngine;

public class PlayerMeleeCombatPrototype : MonoBehaviour
{

    bool inputR1 = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
       inputR1 = Input.GetKeyDown(KeyCode.Joystick1Button5);

        if (inputR1 == true)
        {
            animator.SetTrigger("PressedR1");
        }

    }
}

using UnityEngine;
using System.Collections;

public class CardAnim : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("CardAttack"))
            animator.gameObject.GetComponentInParent<Card>().AttackComplete();
        else if (stateInfo.IsName("CardDestroy"))
            animator.gameObject.GetComponentInParent<Card>().CardDestroyComplete();
    }
}

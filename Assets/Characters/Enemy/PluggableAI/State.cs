using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public AiAction[] actions;
    public Transition[] transitions;

    public void UpdateState(AiController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(AiController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(AiController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}

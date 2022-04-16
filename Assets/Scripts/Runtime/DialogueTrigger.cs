using UnityEngine;
using UnityEngine.Events;

namespace TeamMelon.Runtime
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] protected string[] DialogueLines;
        [SerializeField] protected UnityEvent _onDialogueEnd;

        [Tooltip("Only triggers one time.")]
        [SerializeField] protected bool TriggerOnce = true;

        protected bool Triggered;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (TriggerOnce && Triggered)
            {
                return;
            }

            Triggered = true;
            DialogueManager.Instance.ShowDialogue(DialogueLines, _onDialogueEnd);
        }
    }
}
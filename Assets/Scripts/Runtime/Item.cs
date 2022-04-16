using UnityEngine;
using UnityEngine.Events;

namespace TeamMelon.Runtime
{
    public class Item : DialogueTrigger
    {
        private Transform _player;
        
        private const float GrabDistance = 3.5f;

        private void Start()
        {
            _player = FindObjectOfType<CharacterController>(true).transform;
        }
        
        private void Update()
        {
            if (TriggerOnce && Triggered) return;
            if (Vector3.Distance(_player.position, transform.position) >= GrabDistance) return;
            DialogueManager.Instance.ShowHint();
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
            }
        }
        
        private void TriggerDialogue()
        {
            DialogueManager.Instance.ShowDialogue(DialogueLines, _onDialogueEnd);
            Triggered = true;
            Destroy(gameObject);
        }
        
        protected override void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
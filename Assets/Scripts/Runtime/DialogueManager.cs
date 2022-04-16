using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TeamMelon
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _dialogueCanvasGroup;
        [SerializeField] private TextMeshProUGUI _dialogueText, _hintText;
        
        private Coroutine _dialogueCoroutine;

        private bool _skip;
        
        public static DialogueManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            _dialogueCanvasGroup.alpha = 0;
        }
        
        public void ShowDialogue(IEnumerable<string> messages, UnityEvent onComplete = null)
        {
            _dialogueCanvasGroup.DOFade(1f, 0.5f);
            
            IEnumerator DialogueCoroutine()
            {
                foreach (var message in messages)  
                {
                    _dialogueText.text = "";
                    yield return new WaitForSeconds(0.5f);
                    foreach (var letter in message)
                    {
                        _dialogueText.text += letter;
                        yield return WaitAndCheckForInput();
                        if (!_skip) continue;
                        break;
                    }

                    if (!_skip)
                    {
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
                    } else _skip = false;
                }

                onComplete?.Invoke();
                _dialogueCanvasGroup.DOFade(0f, 0.5f);

                _dialogueCoroutine = null;
            }

            if (_dialogueCoroutine != null) StopCoroutine(_dialogueCoroutine);
            _dialogueCoroutine = StartCoroutine(DialogueCoroutine());
        }

        public void ShowHint()
        {
            _hintText.DOFade(1f, 0.5f).OnComplete(() => _hintText.DOFade(0f, 0.5f));
        }
        
        private IEnumerator WaitAndCheckForInput(float duration = 0.025f)
        {
            var time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _skip = true;
                    yield break;
                }
                yield return null;
            }
        }
    }
}

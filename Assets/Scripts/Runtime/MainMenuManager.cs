using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TeamMelon
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CanvasGroup _canvasGroup, _endingCanvasGroup;
        [SerializeField] private GameObject[] _menuObjects, _playerObjects;
        [SerializeField] private Image _fadeImage;
    
        private void Start()
        {
            
        }

        public void PlayGame()
        {
            _animator.enabled = true;

            IEnumerator Wait()
            {
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.DOFade(0, 1);
                yield return new WaitForSeconds(6f);
                _fadeImage.DOFade(1f, 1f);
                yield return new WaitForSeconds(1f);
                _fadeImage.DOFade(0, 1f);
                foreach (var menuObject in _menuObjects)
                {
                    menuObject.SetActive(false);
                }
                foreach (var playerObject in _playerObjects)
                {
                    playerObject.SetActive(true);
                }
            }

            StartCoroutine(Wait());
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void TriggerEnding()
        {
            foreach (var menuObject in _menuObjects)
            {
                menuObject.SetActive(true);
            }
            foreach (var playerObject in _playerObjects)
            {
                playerObject.SetActive(false);
            }
            _endingCanvasGroup.interactable = true;
            _endingCanvasGroup.blocksRaycasts = true;
            _endingCanvasGroup.DOFade(1f, 1f);
        }

        public void BackToMenu()
        {
            _fadeImage.DOFade(1f, 1f);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(0, 1f).OnComplete(() =>
            {
                _endingCanvasGroup.interactable = true;
                _endingCanvasGroup.blocksRaycasts = true;
                _endingCanvasGroup.DOFade(1f, 1f);
            });
        }
    }
}

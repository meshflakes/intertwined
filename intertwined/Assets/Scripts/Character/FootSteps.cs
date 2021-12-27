using UnityEngine;

namespace Character
{
    public class FootSteps : MonoBehaviour
    {
        [SerializeField] private AudioClip[] grassClips;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Step()
        {
            var clip = GetRandomClip();
            _audioSource.PlayOneShot(clip);
        }

        private AudioClip GetRandomClip()
        {
            return grassClips[Random.Range(0, grassClips.Length)];
        }
    }
}
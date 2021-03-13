using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace naichilab.EasySoundPlayer.Scripts
{
    public class SeAudioSourceController : MonoBehaviour
    {
        [Range(1, 30)] [SerializeField] private int MaxAudioSourceCount = 10;

        private readonly List<AudioSource> _audioSources = new List<AudioSource>();

        void Awake()
        {
            //AudioSourceの生成
            var go = new GameObject("AudioSources");
            go.transform.parent = transform;
            for (var i = 0; i < MaxAudioSourceCount; i++)
            {
                var source = go.AddComponent<AudioSource>();
                source.playOnAwake = false;
                source.loop = false;
                source.volume = 0.5f;
                _audioSources.Add(source);
            }
        }

        internal AudioSource GetFirstAudioSource() => _audioSources.First();

        public AudioSource GetFirstFreeAudioSource()
        {
            return _audioSources.FirstOrDefault(source => !source.isPlaying);
        }

        public void SetVolume(float newVolume)
        {
            foreach (var audioSource in _audioSources)
            {
                audioSource.volume = Mathf.Clamp01(newVolume);
            }
        }
    }
}
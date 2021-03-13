using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace naichilab.EasySoundPlayer.Scripts
{
    [RequireComponent(typeof(SeAudioSourceController))]
    public class SePlayer : MonoBehaviour
    {
        private SeAudioSourceController _seAudioSourceController;
        private readonly Dictionary<string, int> _nameToIndex = new Dictionary<string, int>();
        [SerializeField] private bool DontDestroyOnLoadOnAwake = false;
        [SerializeField] private List<AudioClip> SeList;

        public IEnumerable<string> SeNames
        {
            get
            {
                if (SeList == null) yield break;
                foreach (var clip in SeList)
                {
                    yield return clip.name;
                }
            }
        }

        public float Volume
        {
            get => _seAudioSourceController.GetFirstAudioSource().volume;
            set => _seAudioSourceController.SetVolume(value);
        }

        public void Awake()
        {
            if (this != Instance)
            {
                Destroy(this);
                return;
            }

            for (var i = 0; i < SeList.Count; i++)
            {
                var clip = SeList[i];
                if (!_nameToIndex.ContainsKey(clip.name))
                {
                    _nameToIndex.Add(clip.name, i);
                }
            }

            _seAudioSourceController = GetComponent<SeAudioSourceController>();
            if (DontDestroyOnLoadOnAwake)
            {
                DontDestroyOnLoad(gameObject);
            }
        }


        public void Play(string seName)
        {
            if (!_nameToIndex.ContainsKey(seName))
            {
                Debug.LogError("存在しないSE名を指定されたため再生できません。");
                return;
            }

            Play(_nameToIndex[seName]);
        }

        public void Play(int seIndex = 0)
        {
            if (!SeList.Any())
            {
                Debug.LogError("SeListにAudioClipが１つも設定されていないため再生できません。");
                return;
            }

            if (seIndex < 0 || SeList.Count <= seIndex)
            {
                Debug.LogError("存在しないSE番号を指定されたため再生できません。");
                return;
            }

            var audioSource = _seAudioSourceController.GetFirstFreeAudioSource();
            if (audioSource == null)
            {
                Debug.LogWarning("同時再生数の上限に達したため再生できません。");
                return;
            }

            //新規再生
            var clip = SeList[seIndex];
            audioSource.clip = clip;
            audioSource.Play();
        }


        #region Singleton

        private static SePlayer _instance;

        public static SePlayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (SePlayer) FindObjectOfType(typeof(SePlayer));
                }

                if (_instance == null)
                {
                    Debug.LogWarning("SePlayerが見つかりませんでした。");
                }

                return _instance;
            }
        }

        #endregion
    }
}
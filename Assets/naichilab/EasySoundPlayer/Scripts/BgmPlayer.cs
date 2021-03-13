using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace naichilab.EasySoundPlayer.Scripts
{
    public class BgmPlayer : MonoBehaviour
    {
        private AudioSource _audioSource;
        private readonly Dictionary<string, int> _nameToIndex = new Dictionary<string, int>();
        private int _currentBgmIndex = -1;
        [SerializeField] private bool PlayOnAwake = true;
        [SerializeField] private bool DontDestroyOnLoadOnAwake = false;
        [SerializeField] private List<AudioClip> BgmList;

        public IEnumerable<string> BgmNames
        {
            get
            {
                if (BgmList == null) yield break;
                foreach (var clip in BgmList)
                {
                    yield return clip.name;
                }
            }
        }


        public float Volume
        {
            get => _audioSource.volume;
            set => _audioSource.volume = value;
        }

        public void Awake()
        {
            if (this != Instance)
            {
                Destroy(this);
                return;
            }

            for (var i = 0; i < BgmList.Count; i++)
            {
                var clip = BgmList[i];
                if (!_nameToIndex.ContainsKey(clip.name))
                {
                    _nameToIndex.Add(clip.name, i);
                }
            }

            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.Stop();
            _audioSource.clip = null;
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
            _audioSource.volume = 0.5f;

            if (DontDestroyOnLoadOnAwake)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Start()
        {
            if (PlayOnAwake)
            {
                Play(0);
            }
        }

        public void Play(string bgmName)
        {
            if (!_nameToIndex.ContainsKey(bgmName))
            {
                Debug.LogError("存在しないBGM名を指定されたため再生できません。");
                return;
            }

            Play(_nameToIndex[bgmName]);
        }

        public void Play(int bgmIndex = 0)
        {
            if (!BgmList.Any())
            {
                Debug.LogError("BgmListにAudioClipが１つも設定されていないため再生できません。");
                return;
            }

            if (bgmIndex < 0 || BgmList.Count <= bgmIndex)
            {
                Debug.LogError("存在しないBGM番号を指定されたため再生できません。");
                return;
            }

            if (bgmIndex == _currentBgmIndex)
            {
                //再開
                if (!_audioSource.isPlaying) _audioSource.Play();
            }
            else
            {
                //新規再生
                var clip = BgmList[bgmIndex];
                if (_audioSource.isPlaying) _audioSource.Stop();
                _audioSource.clip = clip;
                _audioSource.Play();
                _currentBgmIndex = bgmIndex;
            }
        }

        public void Pause()
        {
            if (_audioSource.isPlaying) _audioSource.Pause();
        }

        public void Stop()
        {
            _audioSource.Stop();
            _currentBgmIndex = -1;
        }


        #region Singleton

        private static BgmPlayer _instance;

        public static BgmPlayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (BgmPlayer) FindObjectOfType(typeof(BgmPlayer));
                }

                if (_instance == null)
                {
                    Debug.LogWarning("BgmPlayerが見つかりませんでした。");
                }

                return _instance;
            }
        }

        #endregion
    }
}
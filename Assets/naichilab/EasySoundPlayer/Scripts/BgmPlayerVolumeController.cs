using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace naichilab.EasySoundPlayer.Scripts
{
    public class BgmPlayerVolumeController : MonoBehaviour
    {
        [SerializeField] private Slider Slider;
        public FloatReactiveProperty CurrentVolume = new FloatReactiveProperty(0.5f);

        void Start()
        {
            var bgmPlayer = BgmPlayer.Instance;

            if (bgmPlayer == null)
            {
                Debug.LogWarning("BgmPlayerが見つかりませんでした。");
                return;
            }

            CurrentVolume
                .Subscribe(newVolume => bgmPlayer.Volume = newVolume)
                .AddTo(this);

            if (Slider == null) return;

            CurrentVolume
                .Subscribe(newVolume => Slider.value = Mathf.Clamp01(newVolume))
                .AddTo(this);

            Slider.OnValueChangedAsObservable()
                .Subscribe(newVolume => CurrentVolume.Value = newVolume)
                .AddTo(this);
            Slider.value = bgmPlayer.Volume;
        }
    }
}
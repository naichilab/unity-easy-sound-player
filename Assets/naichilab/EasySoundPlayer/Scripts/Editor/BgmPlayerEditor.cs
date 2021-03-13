using System.Linq;
using UnityEditor;
using UnityEngine;

namespace naichilab.EasySoundPlayer.Scripts.Editor
{
    [CustomEditor(typeof(BgmPlayer))]
    public class BgmPlayerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var bgmPlayer = target as BgmPlayer;

            if (!bgmPlayer.BgmNames.Any())
            {
                EditorGUILayout.HelpBox("BgmListにAudioClipを１つ以上セットしてください。", MessageType.Error);
                return;
            }

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("エディター実行中はここにBGMの再生や停止などを行うボタンが表示されます。", MessageType.Info);
                return;
            }


            //再生ボタン
            foreach (var bgmName in bgmPlayer.BgmNames)
            {
                if (GUILayout.Button($"Play : {bgmName}"))
                {
                    bgmPlayer.Play(bgmName);
                }
            }

            //一時停止ボタン
            if (GUILayout.Button($"Pause"))
            {
                bgmPlayer.Pause();
            }

            //停止ボタン
            if (GUILayout.Button($"Stop"))
            {
                bgmPlayer.Stop();
            }
        }
    }
}
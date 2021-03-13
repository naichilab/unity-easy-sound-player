using System.Linq;
using UnityEditor;
using UnityEngine;

namespace naichilab.EasySoundPlayer.Scripts.Editor
{
    [CustomEditor(typeof(SePlayer))]
    public class SePlayerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var sePlayer = target as SePlayer;

            if (!sePlayer.SeNames.Any())
            {
                EditorGUILayout.HelpBox("SeListにAudioClipを１つ以上セットしてください。", MessageType.Error);
                return;
            }

            if (!EditorApplication.isPlaying)
            {
                EditorGUILayout.HelpBox("エディター実行中はここにSEの再生を行うボタンが表示されます。", MessageType.Info);
                return;
            }

            //再生ボタン
            foreach (var seName in sePlayer.SeNames)
            {
                if (GUILayout.Button($"Play : {seName}"))
                {
                    sePlayer.Play(seName);
                }
            }
        }
    }
}
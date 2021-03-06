using System;
using UnityEngine;

namespace DOTSAnimation.Authoring
{
    [Serializable]
    public struct AnimationClipEvent
    {
        public AnimationEventName Name;
        [Range(0,1)]
        public float NormalizedTime;

        public int Hash => Name.Hash;
    }
    
    [CreateAssetMenu(menuName = "Tools/DOTSAnimation/Clip")]
    public class AnimationClipAsset : ScriptableObject
    {
        public AnimationClip Clip;
        public AnimationClipEvent[] Events;
    }
}
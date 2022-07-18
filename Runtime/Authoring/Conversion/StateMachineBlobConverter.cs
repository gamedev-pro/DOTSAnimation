using Latios.Authoring.Systems;
using Latios.Kinemation;
using Latios.Unsafe;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;

namespace DOTSAnimation.Authoring
{
    internal struct TransitionGroupConvertData
    {
        internal short FromStateIndex;
        internal short ToStateIndex;
        internal float NormalizedTransitionDuration;
        internal UnsafeList<BoolTransition> BoolTransitions;
    }

    internal struct AnimationClipEventsBlobConvertData
    {
        internal UnsafeList<ClipEventBlob> Events;
    }
    internal struct StateMachineBlobConverter : ISmartBlobberSimpleBuilder<StateMachineBlob>
    {
        internal UnsafeList<SingleClipStateBlob> SingleClipStates;
        internal UnsafeList<AnimationStateBlob> States;
        internal UnsafeList<StateMachineParameter> Parameters;
        internal UnsafeList<TransitionGroupConvertData> Transitions;
        internal UnsafeList<AnimationClipEventsBlobConvertData> Events;

        public unsafe BlobAssetReference<StateMachineBlob> BuildBlob()
        {
            var builder = new BlobBuilder(Allocator.Temp);
            ref var root = ref builder.ConstructRoot<StateMachineBlob>();
            builder.ConstructFromNativeArray(ref root.SingleClipStates, SingleClipStates.Ptr, SingleClipStates.Length);
            builder.ConstructFromNativeArray(ref root.States, States.Ptr, States.Length);
            builder.ConstructFromNativeArray(ref root.Parameters, Parameters.Ptr, Parameters.Length);
            
            var transitions = builder.Allocate(ref root.Transitions, Transitions.Length);
            for (short i = 0; i < transitions.Length; i++)
            {
                var transitionConvertData = Transitions[i];
                transitions[i] = new DOTSAnimation.AnimationTransitionGroup()
                {
                    NormalizedTransitionDuration = transitionConvertData.NormalizedTransitionDuration,
                    FromStateIndex = transitionConvertData.FromStateIndex,
                    ToStateIndex = transitionConvertData.ToStateIndex
                };
                builder.ConstructFromNativeArray(ref transitions[i].BoolTransitions,
                    transitionConvertData.BoolTransitions.Ptr, transitionConvertData.BoolTransitions.Length);
            }

            var clipEvents = builder.Allocate(ref root.ClipEvents, Events.Length);
            for (short i = 0; i < clipEvents.Length; i++)
            {
                clipEvents[i] = default;
                var clipEventsConvertData = Events[i];
                builder.ConstructFromNativeArray(ref clipEvents[i].Events, clipEventsConvertData.Events.Ptr, clipEventsConvertData.Events.Length);
            }
            
            return builder.CreateBlobAssetReference<StateMachineBlob>(Allocator.Persistent);
        }
    }
}
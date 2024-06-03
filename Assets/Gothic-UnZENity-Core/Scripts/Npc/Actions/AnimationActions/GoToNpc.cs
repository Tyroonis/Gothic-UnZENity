using GUZ.Core.Caches;
using GUZ.Core.Data.ZkEvents;
using UnityEngine;

namespace GUZ.Core.Npc.Actions.AnimationActions
{
    public class GoToNpc : AbstractWalkAnimationAction
    {
        private Transform destinationTransform;
        private int otherId => Action.Int0;
        private int otherIndex => Action.Int1;

        public GoToNpc(AnimationAction action, GameObject npcGo) : base(action, npcGo)
        {
        }

        public override void Start()
        {
            base.Start();

            destinationTransform = LookupCache.NpcCache[otherIndex].transform;
        }

        protected override Vector3 GetWalkDestination()
        {
            return destinationTransform.position;
        }


        public override void AnimationEndEventCallback(SerializableEventEndSignal eventData)
        {
            base.AnimationEndEventCallback(eventData);

            IsFinishedFlag = false;
        }

        protected override void OnDestinationReached()
        {
            AnimationEndEventCallback(new SerializableEventEndSignal(nextAnimation: ""));

            walkState = WalkState.Done;
            IsFinishedFlag = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LifeContent;

namespace Anim{
    public enum Gesture{
        Hi,
        Success,
        Fail
    }

    public class PlayerAction
    {
        public Gesture gesture;
        public FarmingType farming;
        public WoodcuttingType wood;
        public FishingType fishing;
        public MiningType mining;
        
    }
}
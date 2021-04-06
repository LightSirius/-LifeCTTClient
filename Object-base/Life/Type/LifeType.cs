namespace LifeContent {
    // 생활 종류
    public enum LifeType{
        Woodcutting = 0,
        Farming,
        Fishing,
        Mining,
        Livestock,
        Count
    }

    public enum FarmingType{
        GroundPlant = 0,         // 꽃, 버섯 등 땅 밖에서 자라는 것들
        UnderGroundPlant,        // 감자, 산삼 등 땅속에서 자라는 것들
        Count
    }

    public enum FishingType{
        Rod = 0,        // 낚시대
        Net,        // 그물망
        Count
    }

    public enum LivestockType{
        Meat = 0,
        Leather,
        ByProduct,
        Count
    }

    public enum MiningType{
        Pick = 0,
        Count
    }

    public enum WoodcuttingType{
        Tree = 0,
        FruitTree,
        FlowerTree,
        Count
    }   

    public enum Climate{
        Temperate = 0,      // 온대 기후
        Microthermal,       // 냉대 기후
        Polar,              // 한대 기후
        Tropical,           // 열대 기후
        Dry,                // 건조 기후
        Alpine,             // 고산 기후
        Count
    }

    public enum Weather{
        Sun = 0,
        Cloudy,     // 구름 많음(흐림)
        Rain,
        Snow,
        Hail,       // 우박
        Fog,
        Count
    }
}
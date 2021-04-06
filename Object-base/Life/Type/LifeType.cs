namespace LifeContent {
    // ��Ȱ ����
    public enum LifeType{
        Woodcutting = 0,
        Farming,
        Fishing,
        Mining,
        Livestock,
        Count
    }

    public enum FarmingType{
        GroundPlant = 0,         // ��, ���� �� �� �ۿ��� �ڶ�� �͵�
        UnderGroundPlant,        // ����, ��� �� ���ӿ��� �ڶ�� �͵�
        Count
    }

    public enum FishingType{
        Rod = 0,        // ���ô�
        Net,        // �׹���
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
        Temperate = 0,      // �´� ����
        Microthermal,       // �ô� ����
        Polar,              // �Ѵ� ����
        Tropical,           // ���� ����
        Dry,                // ���� ����
        Alpine,             // ��� ����
        Count
    }

    public enum Weather{
        Sun = 0,
        Cloudy,     // ���� ����(�帲)
        Rain,
        Snow,
        Hail,       // ���
        Fog,
        Count
    }
}
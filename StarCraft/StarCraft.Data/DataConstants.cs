namespace StarCraft.Data
{
    public static class DataConstants
    {
        public const int MaxByteImageSize = 5 * 1024 * 1024;

        // Building constats
        public const int MinBuildingNameLength = 2;

        public const int MaxBuildingNameLength = 100;

        public const int MinMineralBuildingCost = 50;
        public const int MaxMineralBuildingCost = 2000;

        public const int MinGasBuildingCost = 0;
        public const int MaxGasBuildingCost = 2000;

        // User constats
        public const int MinUserMineralCapacity = 0;

        public const int MaxUserMineralCapacity = 100000;

        public const int MinUserGasCapacity = 0;
        public const int MaxUserGasCapacity = 100000;

        public const int ExpForLevelTwo = 150;
        public const int ExpForLevelThree = 250;
        public const int ExpForLevelFour = 400;
        public const int ExpForLevelFive = 600;
        public const int ExpForLevelSix = 850;
        public const int ExpForLevelSeven = 1150;
        public const int ExpForLevelEight = 1500;
        public const int ExpForLevelNine = 2000;
        public const int ExpForLevelTen = 3000;

        public const int UserStartExp = 0;
        public const int UserStartLevel = 0;

        public const int UserMinExp = 0;
        public const int UserMaxExp = 1000;

        public const int UserMinLevel = 0;
        public const int UserMaxLevel = 11;

        public const int UserStartingMinerals = 250;
        public const int UserStartingGas = 0;

        // Unit constats
        public const int MinUnitNameLenght = 3;

        public const int MaxUnitNameLenght = 100;

        public const int MinMineralUnitCost = 50;
        public const int MaxMineralUnitCost = 2000;

        public const int MinGasUnitCost = 0;
        public const int MaxGasUnitCost = 2000;

        public const int MinUnitHealth = 1;
        public const int MaxUnitHealth = 5000;

        public const int MinUnitDamage = 1;
        public const int MaxUnitDamage = 500;
    }
}
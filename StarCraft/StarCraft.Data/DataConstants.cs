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
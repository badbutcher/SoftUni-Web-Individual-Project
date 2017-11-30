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
    }
}
namespace Data
{
    public enum AllianceType
    {
        Player, Foe, Ally
    }

    public static class AllianceExtensions
    {
        public static bool IsFriendly(this AllianceType self, AllianceType other)
        {
            if (self == AllianceType.Player || self == AllianceType.Ally) return other == AllianceType.Player || other == AllianceType.Ally;
            return other == AllianceType.Foe;
        }
    }
}
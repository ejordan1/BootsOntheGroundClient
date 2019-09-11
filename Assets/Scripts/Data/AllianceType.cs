namespace Data
{
    public enum AllianceType //make multiple teams here
    {
        Team1, Team2, Team3, Team4, Team5, Team6, Team7, Team8
    }

    public static class AllianceExtensions
    {
        public static bool IsFriendly(this AllianceType self, AllianceType other)
        {
			return self == other;
        }
    }
}



/*
 * 
 * public enum AllianceType //make multiple teams here
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

*/
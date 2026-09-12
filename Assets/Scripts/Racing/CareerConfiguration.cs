using UnityEngine;

[CreateAssetMenu(fileName = "CareerConfig", menuName = "Racing/Career Configuration")]
public class CareerConfiguration : ScriptableObject
{
    [System.Serializable]
    public class RaceEvent
    {
        public int eventId;
        public string eventName;
        public string description;
        public RaceMode raceMode;
        public int lapCount;
        public int difficulty; // 1-3
        public int rewardCredits;
        public int rewardExperience;
        public int requiredLevel;
        public bool isBossRace;
    }
    
    [System.Serializable]
    public class CareerTier
    {
        public string tierName;
        public int minLevel;
        public int maxLevel;
        public RaceEvent[] races;
    }
    
    [SerializeField] private CareerTier[] careerTiers;
    [SerializeField] private int startingCredits = 100000;
    [SerializeField] private int creditsPerLevel = 10000;
    
    public CareerTier[] GetCareerTiers() => careerTiers;
    public int GetStartingCredits() => startingCredits;
    public int GetCreditsPerLevel() => creditsPerLevel;
    
    public RaceEvent GetRaceEventById(int eventId)
    {
        foreach (CareerTier tier in careerTiers)
        {
            foreach (RaceEvent evt in tier.races)
            {
                if (evt.eventId == eventId)
                    return evt;
            }
        }
        return null;
    }
}

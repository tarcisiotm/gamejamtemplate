using UnityEngine;

[CreateAssetMenu(fileName = "Credits", menuName = "Data/Create Credits Asset")]
public class CreditsData : ScriptableObject
{
    [SerializeField] string _title;
    [SerializeField] CreditsEntryData[] _creditsEntries;

    public string Title => _title;
    public CreditsEntryData[] CreditsEntries => _creditsEntries;
}

[System.Serializable]
public class CreditsEntryData
{
    public string Name;
    public string[] Links;
}
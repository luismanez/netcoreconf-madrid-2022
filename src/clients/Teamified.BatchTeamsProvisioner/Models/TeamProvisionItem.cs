namespace Teamified.BatchTeamsProvisioner.Models;

internal class TeamProvisionItem
{
    public string DisplayName { get; set; }
    public string Description { get; set; }

    public TeamProvisionItem(string displayName, string description)
    {
        DisplayName = displayName;
        Description = description;
    }
}

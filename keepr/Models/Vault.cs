namespace keepr.Models;

public class Vault
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public string CreatorId { get; set; }
  public Boolean IsPrivate { get; set; }

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  // relationship
  public Profile Creator  { get; set; }
}

public class VaultKeepVault : Vault
{
    public int VaultKeepId { get; set; }
}
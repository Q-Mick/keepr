namespace keepr.Models;

public class Keep
{
  public int Id { get; set; }
  public string CreatorId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public int Views { get; set; }
   public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  // count of how many times its kept
    public int Kept { get; set; }
    // relationships 

    public Account Creator { get; set; }
}

public class VaultKeepItem : Keep
{
    public int VaultKeepId { get; set; }
}

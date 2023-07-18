namespace keepr.Services;

public class ProfileService
{
  private readonly ProfilesRepository _repo;
  private readonly KeepsRepository _kr;
  private readonly VaultsRepository _vr;
  public ProfileService(ProfilesRepository repo, KeepsRepository kr, VaultsRepository vr)
  {
    _repo = repo;
    _kr = kr;
    _vr = vr;
  }

  internal Profile GetProfile(string Id)
  {
    return _repo.GetProfile(Id);
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    List<Keep> Keeps = _kr.GetKeepsByProfileId(profileId);
    return Keeps;
  }
  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    List<Vault> Keeps = _vr.GetVaultsByProfileId(profileId);
    return Keeps;
  }



}

namespace keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _kr;

  public KeepsService(KeepsRepository kr)
  {
    _kr = kr;
  }
  internal Keep CreateKeep(Keep KeepData)
  {
    Keep newKeep = _kr.CreateKeep(KeepData);
    return newKeep;
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> Keeps = _kr.GetAllKeeps();
    return Keeps;
  }

  internal Keep GetById(int keepId)
  {
    Keep keep = _kr.GetById(keepId);
    if (keep.Name == "") throw new Exception("Invalid keep");
    if (keep.Id == 0) throw new Exception("Invalid keep");
    keep.Views++;
    _kr.UpdateKeep(keep);
    return keep;
  }
  internal Keep UpdateKeptCount(int keepId, Keep keepData)
  {
    Keep originalKeep = GetById(keepId);
    originalKeep.Kept = originalKeep.Kept != keepData.Kept ? keepData.Kept : originalKeep.Kept;
    _kr.UpdateKeep(originalKeep);

    return originalKeep;
  }
  internal Keep UpdateKeep(int keepId, Keep keepData)
  {
    Keep originalKeep = GetById(keepId);
    if (originalKeep.CreatorId != keepData.CreatorId) throw new Exception("Shoo fly!");
    originalKeep.Name = originalKeep.Name.Length > 0 ? keepData.Name : originalKeep.Name;
    originalKeep.Img = originalKeep.Img.Length > 0 ? keepData.Img : originalKeep.Img;
    originalKeep.Description = originalKeep.Description.Length > 0 ? keepData.Description : originalKeep.Description;
    originalKeep.Views = originalKeep.Views > 0 ? keepData.Views : originalKeep.Views;
    _kr.UpdateKeep(originalKeep);

    return originalKeep;
  }
      internal void DeleteKeep(int keepId, string userId)
    {
        Keep keep = GetById(keepId);
        if (keep.CreatorId != userId) throw new Exception("Not yours buddy");
        int rows = _kr.DeleteKeep(keepId);
        if (rows > 1) throw new Exception("Something went wrong. deleted more than 1");
    }
}

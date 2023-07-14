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
    if (keep.Name == null) new Exception("Invalid keep ID");
    // keep.Views++;
    // _kr.Edit(keep);
    return keep;
  }

  internal Keep UpdateKeep(int keepId, Keep keepData)
  {
    Keep originalKeep = GetById(keepId);
    if (originalKeep.CreatorId != keepData.CreatorId) throw new Exception("Shoo fly!");
    originalKeep.Name = originalKeep.Name.Length > 0 ? keepData.Name : originalKeep.Name;
    originalKeep.Img = originalKeep.Img.Length > 0 ? keepData.Img : originalKeep.Img;
    originalKeep.Description = originalKeep.Description.Length > 0 ? keepData.Description : originalKeep.Description;
    _kr.UpdateKeep(originalKeep);

    return originalKeep;
  }
      internal void DeleteKeep(int keepId, string userId)
    {
        Keep keep = GetById(keepId);
        if (keep.CreatorId != userId) new Exception("Not yours buddy");
        int rows = _kr.DeleteKeep(keepId);
        if (rows > 1) new Exception("Something went wrong. deleted more than 1");
    }
}

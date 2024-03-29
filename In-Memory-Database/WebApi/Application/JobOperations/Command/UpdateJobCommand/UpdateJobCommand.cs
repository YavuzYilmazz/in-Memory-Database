using WebApi.DbOperations;

namespace WebApi.Application.JobOperations.Command.UpdateJobCommand;

public class UpdateJobCommand
{
    public int JobId { get; set; }
    public UpdateJobModel Model { get; set; }
    private readonly IUserDbContext _context;
    public UpdateJobCommand(IUserDbContext context)
    {
        _context = context;
    }


    public void Handle()
    {
        var job = _context.Jobs.SingleOrDefault(x => x.Id == JobId);
        if (job is null)
            throw new InvalidOperationException("Job is not found.");
        if (_context.Jobs.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != JobId))
            throw new InvalidOperationException("This job's name is already exist.");

        job.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? job.Name : Model.Name;
        job.IsActive = Model.IsActive;
        _context.SaveChanges();

    }
}

public class UpdateJobModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
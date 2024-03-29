using WebApi.DbOperations;

namespace WebApi.Application.JobOperations.Command.CreateJobCommand;

public class CreateJobCommand
{
    public CreateJobModel Model { get; set; }
    private readonly UserDbContext _context;

    public CreateJobCommand(UserDbContext context)
    {
        _context = context;
    }


    public void Handle()
    {
        var job = _context.Jobs.SingleOrDefault(x => x.Name == Model.Name);
        if (job is not null)
            throw new InvalidOperationException("Job is already exist.");

        job = new Job();
        job.Name = Model.Name;
        _context.Jobs.Add(job);
        _context.SaveChanges();
    }
}

public class CreateJobModel
{
    public string Name { get; set; }
}
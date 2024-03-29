using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.JobOperations.Queries.GetJobDetail;

public class GetJobDetailQuery
{
    public int JobId;
    public readonly UserDbContext _context;
    public readonly IMapper _mapper;

    public GetJobDetailQuery(UserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public JobDetailViewModel Handle()
    {
        var job = _context.Jobs.SingleOrDefault(x => x.IsActive && x.Id == JobId);
        if (job is null)
            throw new InvalidOperationException("Job is not found.");
        return _mapper.Map<JobDetailViewModel>(job);
    }

    public class JobDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
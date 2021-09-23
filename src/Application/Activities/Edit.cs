using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest<bool>
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);
                if(activity is null)
                {                    
                    return false;
                }

              _mapper.Map(request.Activity,activity);

                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}
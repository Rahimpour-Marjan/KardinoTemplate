using Application.Users.Models;
using AutoMapper;
using Infrastructure.Persistance.Repositories;
using MediatR;

namespace Application.User.Queries.FindById
{
    class FindUserByIdQueryHandler : IRequestHandler<FindUserByIdQuery, UserInfo>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public FindUserByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<UserInfo> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _uow.UserRepository.FindById(request.Id);

            var result = _mapper.Map<Domain.User, UserInfo>(model);

            return result;
        }
    }
}

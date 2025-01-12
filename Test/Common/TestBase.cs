using AutoFixture;
using AutoMapper;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTest.Common
{
    public class TestBase
    {
        //protected readonly Mock<IMediator> _mediator;
        protected readonly Mock<IMediator> _mediator;
        protected readonly Mock<IMapper> _mapper;
        protected readonly IUnitOfWork _uow;
        protected readonly Fixture _fixture;
        public TestBase()
        {
            _mapper = new Mock<IMapper>();
            var options = new DbContextOptionsBuilder<KardinoTemplateDbContext>()
                            .UseSqlServer("Data Source=.;Initial Catalog=KardinoTemplateDb;Persist Security Info=True;User ID=sa;Password=5119185")
                            .Options;
            _uow = new UnitOfWork(new KardinoTemplateDbContext(options));

            _fixture = new Fixture();
        }

    }
}

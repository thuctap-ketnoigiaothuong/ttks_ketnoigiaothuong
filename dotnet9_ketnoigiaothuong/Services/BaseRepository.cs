using AutoMapper;
using dotnet9_ketnoigiaothuong.Infrastructure.Context;

namespace dotnet9_ketnoigiaothuong.Services
{
    public abstract class BaseRepository
    {
        protected AppDbContext context;
        protected IMapper mapper;
        public BaseRepository(AppDbContext context, IMapper mapper)
            => (this.context, this.mapper) = (context, mapper);
    }
}

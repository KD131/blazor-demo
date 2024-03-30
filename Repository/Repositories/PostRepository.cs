using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Repository.Repositories;

public class PostRepository(DemoContext dbContext) : EfCrudRepository<Post, int>(dbContext), IPostRepository
{
    
}
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Repository.Repositories;

public class PostRepository(DbContext dbContext) : EfCrudRepository<Post, int>(dbContext), IPostRepository
{
    
}
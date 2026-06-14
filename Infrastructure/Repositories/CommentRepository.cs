using Application.Abstractions.Persistence;
using Application.Features.Auth.Command.Register;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository(AppDbContext context) : Repository<Comment>(context), ICommentRepository
{
    
}
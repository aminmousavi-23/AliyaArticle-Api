using Application.Abstractions.Persistence;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AttachmentRepository(AppDbContext context) : Repository<Attachment>(context), IAttachmentRepository
{
    
}
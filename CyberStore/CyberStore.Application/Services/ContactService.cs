using CyberStore.Core.Entities;
using CyberStore.Data.Repositories.Interfaces;
using CyberStore.Application.Services.Interfaces;

namespace CyberStore.Application.Services;

public class ContactService(IUnitOfWork unitOfWork) : IContactService
{
    public async Task SubmitMessageAsync(ContactMessage message)
    {
        message.CreatedAt = DateTime.UtcNow;
        message.IsRead = false;
        
        await unitOfWork.ContactMessages.AddAsync(message);
        await unitOfWork.CompleteAsync();
    }
}

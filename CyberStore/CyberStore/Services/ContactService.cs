using CyberStore.Models;
using CyberStore.Repositories.Interfaces;
using CyberStore.Services.Interfaces;

namespace CyberStore.Services;

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

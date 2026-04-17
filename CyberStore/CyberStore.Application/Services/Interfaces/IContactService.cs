using CyberStore.Core.Entities;

namespace CyberStore.Application.Services.Interfaces;

public interface IContactService
{
    Task SubmitMessageAsync(ContactMessage message);
}

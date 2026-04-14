using CyberStore.Models;

namespace CyberStore.Services.Interfaces;

public interface IContactService
{
    Task SubmitMessageAsync(ContactMessage message);
}

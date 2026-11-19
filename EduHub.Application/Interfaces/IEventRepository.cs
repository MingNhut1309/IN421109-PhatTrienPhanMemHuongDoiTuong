using EduHub.Domain.Entities;

namespace EduHub.Application.Interfaces;

public interface IEventRepository
{
    void Add(Event @event);
    Event? GetById(Guid id);
    IEnumerable<Event> GetByClubId(Guid clubId);
    bool Delete(Guid id);
}

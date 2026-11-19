using System;
using System.Collections.Generic;
using EduHub.Application.Interfaces;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Controllers;

[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventRepository _eventRepository;
    private readonly EventService _eventService;

    public EventsController(IEventRepository eventRepository, EventService eventService)
    {
        _eventRepository = eventRepository;
        _eventService = eventService;
    }

    [HttpGet("api/clubs/{clubId:guid}/events")]
    public ActionResult<IEnumerable<Event>> GetByClub(Guid clubId)
    {
        var events = _eventRepository.GetByClubId(clubId);
        return Ok(events);
    }

    [HttpGet("api/events/{id:guid}", Name = "GetEventById")]
    public ActionResult<Event> GetById(Guid id)
    {
        var @event = _eventRepository.GetById(id);
        if (@event == null)
        {
            return NotFound(new { message = $"Không tìm thấy sự kiện với Id: {id}" });
        }
        return Ok(@event);
    }

    [HttpPost("api/clubs/{clubId:guid}/events")]
    public ActionResult<Event> CreateEvent(Guid clubId, [FromBody] CreateEventRequest request)
    {
        if (clubId == Guid.Empty)
        {
            return BadRequest(new { message = "ClubId không được để trống." });
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return BadRequest(new { message = "Tiêu đề không được để trống." });
        }

        if (request.Capacity <= 0)
        {
            return BadRequest(new { message = "Sức chứa phải lớn hơn 0." });
        }

        try
        {
            var created = _eventService.CreateEvent(clubId, request.Title, request.Date, request.Capacity, request.Channel ?? "email");
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("api/events/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _eventRepository.Delete(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Không tìm thấy sự kiện với Id: {id} để xoá." });
        }
        return NoContent();
    }
}

public record CreateEventRequest(string Title, DateTime Date, int Capacity, string? Channel);

using Domain.Roots.Statuses;

namespace ApiApplication.DTOs;

public class StatusDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;
    public int Order { get; set; }

    public static StatusDto Map(Status status)
    {
        return new StatusDto()
        {
            Id = status.Id,
            Name = status.Name,
            Color = status.Color,
            Order = status.Order
        };
    }
}
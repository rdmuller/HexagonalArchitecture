﻿using Domain.ValueObjects;

namespace Domain.Entities;
public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public PersonId? DocumentId { get; set; }
}

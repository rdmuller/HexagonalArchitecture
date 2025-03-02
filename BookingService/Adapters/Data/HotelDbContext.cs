﻿using Domain.Booking.Entities;
using Domain.Guest.Entities;
using Domain.Room.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;
public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Booking> Bookings { get; set; }
    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<Guest> Guests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new GuestConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelDbContext).Assembly);
    }
}

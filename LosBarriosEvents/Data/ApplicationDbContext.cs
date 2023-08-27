using LosBarriosEvents.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LosBarriosEvents.Models;

namespace LosBarriosEvents.Data;

public class ApplicationDbContext : IdentityDbContext<LosBarriosUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<Facility> Facility { get; set; } = default!;
    public DbSet<Room> Rooms { get; set; } = default!;
    public DbSet<Speaker> Speakers { get; set; } = default!;
    public DbSet<Student> Students { get; set; } = default!;
    public DbSet<Volunteer> Volunteers { get; set; } = default!;
    public DbSet<VolunteerWork> VolunteersWork { get; set; } = default!;
    public DbSet<Administration> Administrations { get; set; } = default!;
    public DbSet<EventStudent> EventStudents { get; set; } = default!;
    public DbSet<Equipment> Equipment { get; set; } = default!;
    public DbSet<SpeakerFormEquipment> SpeakerFormEquipment { get; set; } = default!;
    public DbSet<Session> Sessions { get; set; } = default!;
    public DbSet<SpeakerForm> SpeakerForms { get; set; } = default!;
    public DbSet<SessionSpeaker> sessionSpeakers { get; set; } = default!;
    public DbSet<Feedback> Feedback { get; set; } = default!;
    public DbSet<School> Schools {get; set;} = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //Primary Keys for many to many relationships
        builder.Entity<EventStudent>().HasKey(e => new { e.EventId, e.StudentId });
        builder.Entity<SpeakerFormEquipment>().HasKey(e => new { e.SpeakerFormId, e.EquipmentId });
        builder.Entity<SessionSpeaker>().HasKey(e => new { e.SessionId, e.SpeakerId });

        //Map Form keywords to and from string for storage  
        builder.Entity<SpeakerForm>()
            .Property(f => f.Keywords)
            .HasConversion(
                k => string.Join(',', k!),
                k => k.Split(',', StringSplitOptions.RemoveEmptyEntries));
    }

}

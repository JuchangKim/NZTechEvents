//SeedData.cs

using NZTechEvents.Core.Entities;
using NZTechEvents.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

public static class SeedData
{
    public static async Task Initialize(NZTechEventsDbContext context, EventRepository eventRepository)
    {
        // Ensure the SQL Server database is created
        context.Database.EnsureCreated();

        // Seed Users
        SeedUsers(context);

        // Seed Events
        await SeedEvents(eventRepository);
    }

    private static void SeedUsers(NZTechEventsDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Email = "a@a.a", PasswordHash = "Hash123!", Name = "Alice Johnson" },
                new User { Email = "abc@abc.abc", PasswordHash = "Hash456!", Name = "Bob Sanders" },
                new User { Email = "ab@ab.ab", PasswordHash = "Hash789!", Name = "Carol Adams" }
            );
            context.SaveChanges();
        }
    }

    private static async Task SeedEvents(EventRepository eventRepository)
    {
        var events = await eventRepository.GetAllEventsAsync().ToListAsync();
        if (events.Any())
        {
            return; // DB has been seeded
        }

        var eventsToSeed = new Event[]
        {
            new Event
            {
                id = "evt-1001", // Ensure this property is populated
                EventId = "evt-1001",
                Title = "Tech Innovations Expo 2025",
                Date = DateTime.Parse("2025-06-15T09:00:00Z"),
                Location = "Auckland",
                Type = "offline",
                IsFree = false,
                Description = "A large conference featuring startups, etc.",
                Industry = "Technology/Hardware",
                RegistrationLink = "https://example.com/tech-expo-2025",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        id = "1", // Ensure this property is populated
                        CommentDate = DateTime.Parse("2025-04-01T10:00:00Z"),
                        Content = "Looking forward to this event!",
                        EventId = "evt-1001",
                        UserName = "Alice"
                    }
                }
            },
            new Event
            {
                id = "evt-1002", // Ensure this property is populated
                EventId = "evt-1002",
                Title = "AI & Machine Learning Meetup",
                Date = DateTime.Parse("2025-04-02T18:00:00Z"),
                Location = "Wellington",
                Type = "offline",
                IsFree = true,
                Description = "Evening meetup to share AI case studies, demos, and networking opportunities.",
                Industry = "AI/Software",
                RegistrationLink = "https://www.example.com/ai-ml-meetup",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        id = "2", // Ensure this property is populated
                        CommentDate = DateTime.Parse("2025-03-20T08:45:00Z"),
                        Content = "Will there be a Q&A session?",
                        EventId = "evt-1002",
                        UserName = "Rob"
                    }
                }
            },
            new Event
            {
                id = "evt-1003", // Ensure this property is populated
                EventId = "evt-1003",
                Title = "Cloud Summit NZ",
                Date = DateTime.Parse("2025-08-10T10:00:00Z"),
                Location = "Online",
                Type = "online",
                IsFree = false,
                Description = "A virtual summit featuring talks from major cloud providers and hands-on workshops.",
                Industry = "Cloud",
                RegistrationLink = "https://www.example.com/cloud-summit",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        id = "3", // Ensure this property is populated
                        CommentDate = DateTime.Parse("2025-06-15T12:00:00Z"),
                        Content = "Hoping to see a session on hybrid cloud!",
                        EventId = "evt-1003",
                        UserName = "Aiden"
                    }
                }
            },
            new Event
            {
                id = "evt-1004", // Ensure this property is populated
                EventId = "evt-1004",
                Title = "Cybersecurity Workshop",
                Date = DateTime.Parse("2025-03-10T14:00:00Z"),
                Location = "Auckland",
                Type = "both",
                IsFree = true,
                Description = "Hands-on training session on threat detection, risk assessment, and secure coding best practices.",
                Industry = "Security",
                RegistrationLink = "https://www.example.com/cyber-workshop",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        id = "4", // Ensure this property is populated
                        CommentDate = DateTime.Parse("2025-02-25T09:30:00Z"),
                        Content = "Is there a certificate after completion?",
                        EventId = "evt-1004",
                        UserName = "Chris"
                    }
                }
            },
            new Event
            {
                id = "evt-1005", // Ensure this property is populated
                EventId = "evt-1005",
                Title = "AR/VR Hackathon",
                Date = DateTime.Parse("2025-01-25T09:00:00Z"),
                Location = "Christchurch",
                Type = "offline",
                IsFree = false,
                Description = "A 48-hour hackathon to build immersive AR/VR applications.",
                Industry = "AR/VR",
                RegistrationLink = "https://www.example.com/arvr-hack",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        id = "5", // Ensure this property is populated
                        CommentDate = DateTime.UtcNow,
                        Content = "Registered with a team!",
                        EventId = "evt-1005",
                        UserName = "Jay"
                    }
                }
            },
            new Event
            {
                id = "evt-1006", // Ensure this property is populated
                EventId = "evt-1006",
                Title = "Blockchain Conference",
                Date = DateTime.Parse("2025-05-20T09:00:00Z"),
                Location = "Online",
                Type = "online",
                IsFree = true,
                Description = "A virtual conference on blockchain technology, smart contracts, and decentralized finance.",
                Industry = "Blockchain",
                RegistrationLink = "https://www.example.com/blockchain-conf",
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        id = "6", // Ensure this property is populated
                        CommentDate = DateTime.UtcNow,
                        Content = "Can't wait to learn about DeFi!",
                        EventId = "evt-1006",
                        UserName = "Xia"
                    }
                }
            }
        };

        foreach (var evt in eventsToSeed)
        {
            await eventRepository.CreateEventAsync(evt);
        }
    }
}

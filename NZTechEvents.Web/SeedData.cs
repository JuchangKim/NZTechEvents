using NZTechEvents.Core.Entities;
using NZTechEvents.Infrastructure.Data;
using System;
using System.Linq;

public static class SeedData
{
    public static void Initialize(NZTechEventsDbContext context)
    {
        // Seed Users
        SeedUsers(context);

        // Seed Events
        SeedEvents(context);
    }

    private static void SeedEvents(NZTechEventsDbContext context)
    {
        // Check if any events exist
        if (context.Events.Any())
        {
            return;   // DB has been seeded
        }
    
        var eventsToSeed = new Event[]
        {
            new Event
            {
                Id = "evt-1001",
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
                        CommentDate = DateTime.Parse("2025-04-01T10:00:00Z"),
                        Content = "Looking forward to this event!"
                    }
                }
            },
            new Event
            {
                Id = "evt-1002",
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
                        CommentDate = DateTime.Parse("2025-03-20T08:45:00Z"),
                        Content = "Will there be a Q&A session?"
                    }
                }
            },
            new Event
            {
                Id = "evt-1003",
                EventId = "evt-1003",
                Title = "Cloud Summit NZ",
                Date = DateTime.Parse("2025-08-10T10:00:00Z"), // or new DateTime(2025, 8, 10, 10, 0, 0, DateTimeKind.Utc)
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
                        CommentDate = DateTime.Parse("2025-06-15T12:00:00Z"),
                        Content = "Hoping to see a session on hybrid cloud!"
                    }
                }
            },
            new Event
            {
                Id = "evt-1004",
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
                        CommentDate = DateTime.Parse("2025-02-25T09:30:00Z"),
                        Content = "Is there a certificate after completion?"
                    }
                }
            },
            new Event
            {
                Id = "evt-1005",
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
                        // e.g. no content or date yet
                        CommentDate = DateTime.UtcNow,
                        Content = "Registered with a team!"
                    }
                }
            },
            new Event
            {
                Id = "evt-1006",
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
                        CommentDate = DateTime.UtcNow,
                        Content = "Can't wait to learn about DeFi!"
                    }
                }
            }
            // More events if needed
            // Add more events as needed
        };

        foreach (var evt in eventsToSeed)
        {
            context.Events.Add(evt);
        }

        context.SaveChanges();
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
}

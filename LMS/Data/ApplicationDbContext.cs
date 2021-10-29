using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MessageThread>().HasKey(table => new
            {
                table.IDMessageInitial,
                table.IDMessageReply
            });
        }
        public DbSet<Register> registers { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<Chapter> chapters { get; set; }
        public DbSet<Section> sections { get; set; }
        public DbSet<Stage> stages { get; set; }
        public DbSet<Level> levels { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<HoDs> HoDs { get; set; }
        public DbSet<Admin> admins { get; set; }


        public DbSet<Course> courses { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<QuizDetails> QuizDetails { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Message> message { get; set; }
        public DbSet<MessageThread> messageThread { get; set; }
        public DbSet<Events> events { get; set; }
        public DbSet<Tasks> tasks { get; set; }
        public DbSet<Calendar> calendars { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        
        public DbSet<Zone> Zones { get; set; }

        public DbSet<Content> Contents { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using Data_Access_Layer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MySql;




// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data_Access_Layer.Repository
{
    public partial class CompetenceDbContext : DbContext
    {
        public CompetenceDbContext()
        {
        }

        public CompetenceDbContext(DbContextOptions<CompetenceDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Domain> Domain { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<UserType> UserType { get; set; }

        public virtual DbSet<ElementTranslation> ElementTranslation { get; set; }

        public virtual DbSet<DomainCompose> DomainCompose { get; set; }

        public virtual DbSet<SubDomain> SubDomain { get; set; }

        public virtual DbSet<TestStatus> TestStatus { get; set; }

        public virtual DbSet<Answer> Answer { get; set; }

        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }

        public virtual DbSet<TestCategory> TestCategory { get; set; }

        public virtual DbSet<Test> Test { get; set; }

        public virtual DbSet<AnswerQuestion> AnswerQuestion { get; set; }

        public virtual DbSet<QuizCompose> QuizCompose { get; set; }

        public virtual DbSet<TestCompose> TestCompose { get; set; }

        public virtual DbSet<TestUser> TestUser { get; set; }

        public virtual DbSet<QuizUser> QuizUser { get; set; }

        public virtual DbSet<QuestionUser> QuestionUser { get; set; }

        public virtual DbSet<AnswerUser> AnswerUser { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=145.239.0.38;user id=skillquizusr;Pwd=SkillQuiz5!;;port=3310; database=skillquizdb;";

            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            //string? connectionString = ConfigurationManager.AppSettings["countoffiles"];

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.


                //optionsBuilder.UseMySql("Data Source=DESKTOP-V1IICF3\\SQLEXPRESS;Initial Catalog=PersonDatabase;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                        .Property(p => p.Weight)
                        .HasPrecision(5);
            //modelBuilder.Entity<Person>(entity =>
            //{
            //    entity.Property(e => e.Address).IsUnicode(false);

            //    entity.Property(e => e.FirstName).IsUnicode(false);

            //    entity.Property(e => e.LastName).IsUnicode(false);

            //    entity.Property(e => e.PhoneNumber).IsUnicode(false);
            //});

            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

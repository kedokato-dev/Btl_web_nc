using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Btl_web_nc.Models
{
    public partial class NewsletterDBContext : DbContext
    {
        public NewsletterDBContext()
        {
        }

        public NewsletterDBContext(DbContextOptions<NewsletterDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; } = null!;
        public virtual DbSet<Newsletter> Newsletters { get; set; } = null!;
        public virtual DbSet<SentMail> SentMails { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserTopicSubscription> UserTopicSubscriptions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=NewsletterDB;user=root;password=Quan190@", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .UseCollation("utf8mb4_unicode_ci");

                entity.HasMany(d => d.Topics)
                    .WithMany(p => p.Articles)
                    .UsingEntity<Dictionary<string, object>>(
                        "ArticleTopic",
                        l => l.HasOne<Topic>().WithMany().HasForeignKey("TopicId").HasConstraintName("ArticleTopics_ibfk_2"),
                        r => r.HasOne<Article>().WithMany().HasForeignKey("ArticleId").HasConstraintName("ArticleTopics_ibfk_1"),
                        j =>
                        {
                            j.HasKey("ArticleId", "TopicId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("ArticleTopics");

                            j.HasIndex(new[] { "TopicId" }, "TopicId");
                        });
            });

            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.HasIndex(e => e.ServiceId, "ServiceId");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .UseCollation("utf8mb4_unicode_ci");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Newsletters)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("Newsletters_ibfk_1");
            });

            modelBuilder.Entity<SentMail>(entity =>
            {
                entity.HasIndex(e => e.NewsletterId, "NewsletterId");

                entity.HasIndex(e => e.UserId, "UserId");

                entity.Property(e => e.SentAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Newsletter)
                    .WithMany(p => p.SentMails)
                    .HasForeignKey(d => d.NewsletterId)
                    .HasConstraintName("SentMails_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SentMails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("SentMails_ibfk_2");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.ServiceName, "ServiceName")
                    .IsUnique();

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasIndex(e => e.ServiceId, "ServiceId");

                entity.HasIndex(e => e.UserId, "UserId");

                entity.Property(e => e.SubscribedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("Subscriptions_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Subscriptions_ibfk_2");
            });

            modelBuilder.Entity<Sysdiagram>(entity =>
            {
                entity.HasKey(e => e.DiagramId)
                    .HasName("PRIMARY");

                entity.ToTable("sysdiagrams");

                entity.HasIndex(e => new { e.PrincipalId, e.Name }, "principal_id")
                    .IsUnique();

                entity.Property(e => e.DiagramId).HasColumnName("diagram_id");

                entity.Property(e => e.Definition).HasColumnName("definition");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PrincipalId).HasColumnName("principal_id");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.HasIndex(e => e.TopicName, "TopicName")
                    .IsUnique();

                entity.Property(e => e.TopicName)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "Email")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email).UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb4_unicode_ci");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .UseCollation("utf8mb4_unicode_ci");
            });

            modelBuilder.Entity<UserTopicSubscription>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.TopicId, "TopicId");

                entity.HasIndex(e => e.UserId, "UserId");

                entity.Property(e => e.SubscribedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.UserTopicSubscriptions)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("UserTopicSubscriptions_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTopicSubscriptions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("UserTopicSubscriptions_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

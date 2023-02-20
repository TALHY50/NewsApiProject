﻿using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using webapinews.DATA;
using webapinews.Models;

public partial class NewsApiCodeContext : DbContextWithTriggers
{
    private readonly IConfiguration _configuration;
    public NewsApiCodeContext(DbContextOptions options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<BookMark> BookMarks { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-JVIBG7N;Database=NewsApiCodeFirst;Encrypt=False;Trusted_Connection=True; User ID=sa;Password=1122");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new NewsConfiguration());
        modelBuilder.ApplyConfiguration(new BookMarkConfiguration());

        //modelBuilder.Entity<BookMark>(entity =>
        //{
        //    entity.ToTable("BookMark");

        //    //entity.Property(e => e.CreationDate).HasColumnType("datetime");

        //    entity.HasOne(d => d.News).WithMany(p => p.BookMarks)
        //        .HasForeignKey(d => d.NewsId)
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .HasConstraintName("FK_BookMark_News");

        //    entity.HasOne(d => d.User).WithMany(p => p.BookMarks)
        //        .HasForeignKey(d => d.UserId)
        //        .OnDelete(DeleteBehavior.Cascade)
        //        .HasConstraintName("FK_BookMark_Users");
        //});

        //modelBuilder.Entity<News>(entity =>
        //{
        //    entity.Property(e => e.Aurthor)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //    entity.Property(e => e.CreationDate)
        //        .HasDefaultValueSql("(getdate())")
        //        .HasColumnType("datetime");
        //    entity.Property(e => e.Title)
        //        .HasMaxLength(50)
        //        .IsUnicode(false);
        //});

        //modelBuilder.Entity<User>(entity =>
        //{
        //    entity.Property(e => e.Email).HasMaxLength(50);
        //    entity.Property(e => e.Password).HasMaxLength(50);
        //    entity.Property(e => e.UserName).HasMaxLength(50);
        //});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
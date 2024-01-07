﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScheduleIT.Persistence;

#nullable disable

namespace ScheduleIT.Persistence.Migrations
{
    [DbContext(typeof(ScheduleITDbContext))]
    partial class ScheduleITDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScheduleIT.Domain.Aggregates.Employee.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("_passwordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ScheduleIT.Domain.Aggregates.Team.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TeamLeaderEmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TeamLeaderEmployeeId")
                        .IsUnique();

                    b.ToTable("Team");
                });

            modelBuilder.Entity("ScheduleIT.Domain.Aggregates.Team.TeamMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("TeamId");

                    b.ToTable("TeamMember");
                });

            modelBuilder.Entity("ScheduleIT.Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProcessedOnUtc")
                        .HasDatabaseName("IX_OutboxMessage_ProcessedOnUtc");

                    b.ToTable("OutboxMessage");
                });

            modelBuilder.Entity("ScheduleIT.Domain.Aggregates.Employee.Employee", b =>
                {
                    b.OwnsOne("ScheduleIT.Domain.Aggregates.Employee.FirstName", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FirstName");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("ScheduleIT.Domain.Aggregates.Employee.LastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("LastName");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.OwnsOne("ScheduleIT.Domain.Aggregates.Shared.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("EmployeeId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Email");

                            b1.HasKey("EmployeeId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("Employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();
                });

            modelBuilder.Entity("ScheduleIT.Domain.Aggregates.Team.Team", b =>
                {
                    b.HasOne("ScheduleIT.Domain.Aggregates.Employee.Employee", null)
                        .WithOne()
                        .HasForeignKey("ScheduleIT.Domain.Aggregates.Team.Team", "TeamLeaderEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ScheduleIT.Domain.Aggregates.Shared.NonEmptyText", "Name", b1 =>
                        {
                            b1.Property<Guid>("TeamId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("TeamId");

                            b1.ToTable("Team");

                            b1.WithOwner()
                                .HasForeignKey("TeamId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("ScheduleIT.Domain.Aggregates.Team.TeamMember", b =>
                {
                    b.HasOne("ScheduleIT.Domain.Aggregates.Employee.Employee", null)
                        .WithOne()
                        .HasForeignKey("ScheduleIT.Domain.Aggregates.Team.TeamMember", "EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ScheduleIT.Domain.Aggregates.Team.Team", null)
                        .WithMany("Members")
                        .HasForeignKey("TeamId");

                    b.OwnsOne("ScheduleIT.Domain.Aggregates.Team.TeamMemberRole", "Role", b1 =>
                        {
                            b1.Property<Guid>("TeamMemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Role");

                            b1.HasKey("TeamMemberId");

                            b1.ToTable("TeamMember");

                            b1.WithOwner()
                                .HasForeignKey("TeamMemberId");
                        });

                    b.Navigation("Role")
                        .IsRequired();
                });

            modelBuilder.Entity("ScheduleIT.Domain.Aggregates.Team.Team", b =>
                {
                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}

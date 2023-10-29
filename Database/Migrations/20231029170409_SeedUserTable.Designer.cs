﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20231029170409_SeedUserTable")]
    partial class SeedUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Database.Entities.AccountDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("account");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "admin",
                            Password = "admin",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            Login = "user1",
                            Password = "user1",
                            Role = 1
                        },
                        new
                        {
                            Id = 3,
                            Login = "user2",
                            Password = "user2",
                            Role = 1
                        });
                });

            modelBuilder.Entity("Database.Entities.FileDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("accountid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_type");

                    b.Property<bool>("Shared")
                        .HasColumnType("boolean")
                        .HasColumnName("shared");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("file");
                });

            modelBuilder.Entity("Database.Entities.TokenDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("TokenName")
                        .HasColumnType("uuid")
                        .HasColumnName("token_name");

                    b.Property<int>("AccountId")
                        .HasColumnType("integer")
                        .HasColumnName("accountid");

                    b.Property<bool>("Used")
                        .HasColumnType("boolean")
                        .HasColumnName("used");

                    b.Property<DateTime>("timeStamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.HasKey("Id", "TokenName");

                    b.HasIndex("AccountId");

                    b.ToTable("token");
                });

            modelBuilder.Entity("filetoken", b =>
                {
                    b.Property<int>("file_id")
                        .HasColumnType("integer");

                    b.Property<int>("token_id")
                        .HasColumnType("integer");

                    b.Property<Guid>("token_name")
                        .HasColumnType("uuid");

                    b.HasKey("file_id", "token_id", "token_name");

                    b.HasIndex("token_id", "token_name");

                    b.ToTable("filetoken", (string)null);
                });

            modelBuilder.Entity("Database.Entities.FileDb", b =>
                {
                    b.HasOne("Database.Entities.AccountDb", "Account")
                        .WithMany("Files")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Database.Entities.TokenDb", b =>
                {
                    b.HasOne("Database.Entities.AccountDb", "Account")
                        .WithMany("Tokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("filetoken", b =>
                {
                    b.HasOne("Database.Entities.FileDb", null)
                        .WithMany()
                        .HasForeignKey("file_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.TokenDb", null)
                        .WithMany()
                        .HasForeignKey("token_id", "token_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.AccountDb", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}

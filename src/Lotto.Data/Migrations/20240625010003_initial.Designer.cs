﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Lotto.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lotto.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240625010003_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lotto.Domain.Entities.Advertisements.Advertisement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Advertisements.AdvertisementView", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AdvertisementId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ViewTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.HasIndex("UserId");

                    b.ToTable("AdvertisementViews");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Asset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Chat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<string>("Info")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.FootballGame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AwayTeam")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<string>("HomeTeam")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LigaName")
                        .HasColumnType("text");

                    b.Property<DateTime>("MatchDay")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("FootballGames");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.FootballGameResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AwayTeamScore")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<long>("FootballGameId")
                        .HasColumnType("bigint");

                    b.Property<List<int>>("GoalTimes")
                        .HasColumnType("integer[]");

                    b.Property<int>("HomeTeamScore")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FootballGameId");

                    b.ToTable("FootballGameResults");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<long>("ImageId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.NumberGame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan>("Deadline")
                        .HasColumnType("interval");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("TotalCountOfNumbers")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<List<int>>("WinningNumbers")
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.ToTable("NumberGames");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.PlayFootball", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<long>("FootballGameId")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan>("GoalTime")
                        .HasColumnType("interval");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FootballGameId");

                    b.HasIndex("UserId");

                    b.ToTable("PlayFootballs");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.PlayNumber", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("boolean");

                    b.Property<long>("NumberGameId")
                        .HasColumnType("bigint");

                    b.Property<List<int>>("SelectedNumbers")
                        .HasColumnType("integer[]");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("NumberGameId");

                    b.HasIndex("UserId");

                    b.ToTable("PlayNumbers");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<long>("UserAccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.UserAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<string>("CardNumber")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("DeletedById")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("UpdatedById")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId1");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Advertisements.Advertisement", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.Asset", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Advertisements.AdvertisementView", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.Advertisements.Advertisement", "Advertisement")
                        .WithMany()
                        .HasForeignKey("AdvertisementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lotto.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advertisement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Chat", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.Asset", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.FootballGameResult", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.Games.FootballGame", "FootballGame")
                        .WithMany()
                        .HasForeignKey("FootballGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootballGame");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.Game", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.Asset", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.PlayFootball", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.Games.FootballGame", "FootballGame")
                        .WithMany()
                        .HasForeignKey("FootballGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lotto.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootballGame");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Games.PlayNumber", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.Games.NumberGame", "NumberGame")
                        .WithMany()
                        .HasForeignKey("NumberGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lotto.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NumberGame");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.User", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.UserAccount", b =>
                {
                    b.HasOne("Lotto.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lotto.Domain.Entities.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
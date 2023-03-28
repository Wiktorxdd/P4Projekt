﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Database;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230328085655_initial.Designer.cs
    [Migration("20230328085655_initial")]
========
    [Migration("20230323072614_initial")]
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230323072614_initial.Designer.cs
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Database.Entities.Follow", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("FollowingId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "FollowingId");

                    b.ToTable("Follow");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FollowingId = 2
                        },
                        new
                        {
                            UserId = 2,
                            FollowingId = 1
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Like", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Like");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            PostId = 1
                        },
                        new
                        {
                            UserId = 1,
                            PostId = 2
                        },
                        new
                        {
                            UserId = 2,
                            PostId = 1
                        },
                        new
                        {
                            UserId = 2,
                            PostId = 2
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Login", b =>
                {
                    b.Property<int>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("LoginId");

                    b.ToTable("Login");

                    b.HasData(
                        new
                        {
                            LoginId = 1,
                            Email = "Test1@mail.dk",
                            IsDeleted = false,
                            Password = "password",
                            Role = 0
                        },
                        new
                        {
                            LoginId = 2,
                            Email = "Test2@mail.dk",
                            IsDeleted = false,
                            Password = "password",
                            Role = 1
                        },
                        new
                        {
                            LoginId = 3,
                            Email = "Test3@mail.dk",
                            IsDeleted = false,
                            Password = "password",
                            Role = 1
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Post");

                    b.HasData(
                        new
                        {
                            PostId = 1,
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230328085655_initial.Designer.cs
                            Date = new DateTime(2023, 3, 28, 10, 56, 55, 762, DateTimeKind.Local).AddTicks(5855),
========
                            Date = new DateTime(2023, 3, 23, 8, 26, 14, 482, DateTimeKind.Local).AddTicks(9075),
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230323072614_initial.Designer.cs
                            Desc = "tadnawdnada",
                            IsDeleted = false,
                            Title = "testestestest",
                            UserId = 1
                        },
                        new
                        {
                            PostId = 2,
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230328085655_initial.Designer.cs
                            Date = new DateTime(2023, 3, 28, 10, 56, 55, 762, DateTimeKind.Local).AddTicks(5859),
========
                            Date = new DateTime(2023, 3, 23, 8, 26, 14, 482, DateTimeKind.Local).AddTicks(9078),
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230323072614_initial.Designer.cs
                            Desc = "Woooooo!",
                            IsDeleted = false,
                            Title = "Test!",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.PostLikes", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.ToTable("PostLikes");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Likes = 2
                        },
                        new
                        {
                            PostId = 2,
                            Likes = 2
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.PostTag", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            TagId = 1
                        },
                        new
                        {
                            PostId = 1,
                            TagId = 2
                        },
                        new
                        {
                            PostId = 1,
                            TagId = 3
                        },
                        new
                        {
                            PostId = 2,
                            TagId = 3
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("TagId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tag");

                    b.HasData(
                        new
                        {
                            TagId = 1,
                            Name = "sax"
                        },
                        new
                        {
                            TagId = 2,
                            Name = "fax"
                        },
                        new
                        {
                            TagId = 3,
                            Name = "howdy"
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LoginId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("UserId");

                    b.HasIndex("LoginId")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230328085655_initial.Designer.cs
                            Created = new DateTime(2023, 3, 28, 10, 56, 55, 762, DateTimeKind.Local).AddTicks(5825),
                            IsDeleted = false,
========
                            Created = new DateTime(2023, 3, 23, 8, 26, 14, 482, DateTimeKind.Local).AddTicks(9045),
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230323072614_initial.Designer.cs
                            LoginId = 1,
                            UserName = "tester 1"
                        },
                        new
                        {
                            UserId = 2,
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230328085655_initial.Designer.cs
                            Created = new DateTime(2023, 3, 28, 10, 56, 55, 762, DateTimeKind.Local).AddTicks(5829),
                            IsDeleted = false,
========
                            Created = new DateTime(2023, 3, 23, 8, 26, 14, 482, DateTimeKind.Local).AddTicks(9049),
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230323072614_initial.Designer.cs
                            LoginId = 2,
                            UserName = "222test222"
                        },
                        new
                        {
                            UserId = 3,
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230328085655_initial.Designer.cs
                            Created = new DateTime(2023, 3, 28, 10, 56, 55, 762, DateTimeKind.Local).AddTicks(5832),
                            IsDeleted = false,
========
                            Created = new DateTime(2023, 3, 23, 8, 26, 14, 482, DateTimeKind.Local).AddTicks(9051),
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230323072614_initial.Designer.cs
                            LoginId = 3,
                            UserName = "user 3"
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Follow", b =>
                {
                    b.HasOne("WebApi.Database.Entities.User", "User")
                        .WithMany("Follow")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Like", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Database.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("WebApi.Database.Entities.Like", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Post", b =>
                {
                    b.HasOne("WebApi.Database.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Database.Entities.PostLikes", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Post", "Post")
                        .WithOne("PostLikes")
                        .HasForeignKey("WebApi.Database.Entities.PostLikes", "PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("WebApi.Database.Entities.PostTag", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Database.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Login", "Login")
                        .WithOne("User")
                        .HasForeignKey("WebApi.Database.Entities.User", "LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Login", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Post", b =>
                {
                    b.Navigation("PostLikes");
                });

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.Navigation("Follow");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}

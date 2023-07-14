﻿// <auto-generated />
using BlackBooks.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlackBooks.API.Migrations
{
    [DbContext(typeof(BlackBooksDbContext))]
    [Migration("20230714102946_add-authors")]
    partial class addauthors
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlackBooks.API.Data.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jules Verne"
                        },
                        new
                        {
                            Id = 2,
                            Name = "H.G. Wells"
                        },
                        new
                        {
                            Id = 3,
                            Name = "J.R.R. Tolkien"
                        });
                });

            modelBuilder.Entity("BlackBooks.API.Data.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Title = "Journey to the Center of the Earth"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            Title = "Twenty Thousand Leagues Under the Sea"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            Title = "The Time Machine"
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 2,
                            Title = "The War of the Worlds"
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 3,
                            Title = "The Hobbit"
                        },
                        new
                        {
                            Id = 6,
                            AuthorId = 3,
                            Title = "The Lord of the Rings"
                        });
                });

            modelBuilder.Entity("BlackBooks.API.Data.Entities.Book", b =>
                {
                    b.HasOne("BlackBooks.API.Data.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("BlackBooks.API.Data.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}

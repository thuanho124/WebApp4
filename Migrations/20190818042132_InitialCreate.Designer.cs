﻿// <auto-generated />
using System;
using LibraryManagementWithAuthen.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryManagementWithAuthen.Migrations
{
    [DbContext(typeof(LibDbContext))]
    [Migration("20190818042132_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryManagementWithAuthen.Models.Book", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasMaxLength(30);

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Edition")
                        .HasMaxLength(5);

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<long>("ISBN");

                    b.Property<int>("NumofPages");

                    b.Property<DateTime>("PublicDate");

                    b.Property<string>("Subject")
                        .HasMaxLength(15);

                    b.HasKey("BookID");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("LibraryManagementWithAuthen.Models.Librarian", b =>
                {
                    b.Property<int>("LibrarianID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LibrarianEmail")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LibrarianFirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LibrarianLastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("LibrarianID");

                    b.ToTable("Librarian");
                });

            modelBuilder.Entity("LibraryManagementWithAuthen.Models.RentedBook", b =>
                {
                    b.Property<int>("RentedBookID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookID");

                    b.Property<DateTime>("RentDate");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<int>("StudentID");

                    b.HasKey("RentedBookID");

                    b.HasIndex("BookID");

                    b.HasIndex("StudentID");

                    b.ToTable("RentedBook");
                });

            modelBuilder.Entity("LibraryManagementWithAuthen.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("StudentFirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("StudentLastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("StudentPhoneNumber")
                        .HasMaxLength(20);

                    b.HasKey("StudentID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("LibraryManagementWithAuthen.Models.RentedBook", b =>
                {
                    b.HasOne("LibraryManagementWithAuthen.Models.Book", "Book")
                        .WithMany("RentedBooks")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryManagementWithAuthen.Models.Student", "Student")
                        .WithMany("RentedBooks")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

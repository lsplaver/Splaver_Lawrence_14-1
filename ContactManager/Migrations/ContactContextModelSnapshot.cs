﻿// <auto-generated />
using System;
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactManager.Migrations
{
    [DbContext(typeof(ContactContext))]
    partial class ContactContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContactManager.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Friend"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Coworker"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Family"
                        },
                        new
                        {
                            CategoryId = 4,
                            Name = "Medical"
                        },
                        new
                        {
                            CategoryId = 5,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("ContactManager.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.HasKey("ContactId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            ContactId = 1,
                            CategoryId = 1,
                            DateCreated = new DateTime(2021, 9, 23, 17, 10, 0, 0, DateTimeKind.Unspecified),
                            Email = "john.doe@yahoo.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Organization = "test",
                            Phone = "(541)-534-4923"
                        },
                        new
                        {
                            ContactId = 2,
                            CategoryId = 2,
                            DateCreated = new DateTime(2021, 9, 23, 17, 10, 0, 0, DateTimeKind.Unspecified),
                            Email = "jsmith@gmail.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            Organization = "Random",
                            Phone = "(316)-421-4521"
                        });
                });

            modelBuilder.Entity("ContactManager.Models.Contact", b =>
                {
                    b.HasOne("ContactManager.Models.Category", "Category")
                        .WithMany("Contacts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

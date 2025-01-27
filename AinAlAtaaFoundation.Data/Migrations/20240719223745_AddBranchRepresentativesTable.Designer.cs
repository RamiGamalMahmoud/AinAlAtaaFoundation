﻿// <auto-generated />
using System;
using AinAlAtaaFoundation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AinAlAtaaFoundation.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240719223745_AddBranchRepresentativesTable")]
    partial class AddBranchRepresentativesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("AinAlAtaaFoundation.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int>("ClanId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("clan_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_branches");

                    b.HasIndex("ClanId")
                        .HasDatabaseName("ix_branches_clan_id");

                    b.ToTable("branches", (string)null);
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.BranchRepresentative", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int>("BranchId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("branch_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_branch_representative");

                    b.HasIndex("BranchId")
                        .HasDatabaseName("ix_branch_representative_branch_id");

                    b.ToTable("branch_representative", (string)null);
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.Clan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_clans");

                    b.ToTable("clans", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "البو عباس"
                        },
                        new
                        {
                            Id = 2,
                            Name = "البو باز"
                        },
                        new
                        {
                            Id = 3,
                            Name = "البو أسود"
                        },
                        new
                        {
                            Id = 4,
                            Name = "البو نيسان"
                        },
                        new
                        {
                            Id = 5,
                            Name = "البو بدري"
                        },
                        new
                        {
                            Id = 6,
                            Name = "البو دراج"
                        },
                        new
                        {
                            Id = 7,
                            Name = "البو عيسى"
                        });
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_districts");

                    b.ToTable("districts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "الجزيرة/ الجزء الشمالي"
                        },
                        new
                        {
                            Id = 2,
                            Name = "الملحة"
                        },
                        new
                        {
                            Id = 3,
                            Name = "أبو تونية"
                        },
                        new
                        {
                            Id = 4,
                            Name = "الموالي"
                        },
                        new
                        {
                            Id = 5,
                            Name = "الأبيتر"
                        },
                        new
                        {
                            Id = 6,
                            Name = "الطريشة"
                        },
                        new
                        {
                            Id = 7,
                            Name = "تل الكورة"
                        },
                        new
                        {
                            Id = 8,
                            Name = "تل العورة"
                        },
                        new
                        {
                            Id = 9,
                            Name = "بنات الحسن"
                        },
                        new
                        {
                            Id = 10,
                            Name = "القادسية"
                        },
                        new
                        {
                            Id = 11,
                            Name = "حليحل وسالم"
                        },
                        new
                        {
                            Id = 12,
                            Name = "الضباعي"
                        },
                        new
                        {
                            Id = 13,
                            Name = "البلدية (القاطول)"
                        },
                        new
                        {
                            Id = 14,
                            Name = "المنظمة"
                        },
                        new
                        {
                            Id = 15,
                            Name = "البو رحمن"
                        },
                        new
                        {
                            Id = 16,
                            Name = "الصحن (الإمام)"
                        },
                        new
                        {
                            Id = 17,
                            Name = "المدرسة الأولى"
                        },
                        new
                        {
                            Id = 18,
                            Name = "الزراعة"
                        },
                        new
                        {
                            Id = 19,
                            Name = "الجيل الثائر"
                        },
                        new
                        {
                            Id = 20,
                            Name = "السكك"
                        },
                        new
                        {
                            Id = 21,
                            Name = "حي المعتصم"
                        },
                        new
                        {
                            Id = 22,
                            Name = "حي مدرسة المعتصم"
                        },
                        new
                        {
                            Id = 23,
                            Name = "حي المستشفى"
                        },
                        new
                        {
                            Id = 24,
                            Name = "حي الهادي"
                        },
                        new
                        {
                            Id = 25,
                            Name = "المتوكلية"
                        },
                        new
                        {
                            Id = 26,
                            Name = "معمل الأدوية"
                        },
                        new
                        {
                            Id = 27,
                            Name = "حي الضباط"
                        },
                        new
                        {
                            Id = 28,
                            Name = "حي العرموشية الأولى"
                        },
                        new
                        {
                            Id = 29,
                            Name = "حي القادسية"
                        },
                        new
                        {
                            Id = 30,
                            Name = "العرموشية الثانية"
                        },
                        new
                        {
                            Id = 31,
                            Name = "حي المعلمين"
                        },
                        new
                        {
                            Id = 32,
                            Name = "حي الجبيرية"
                        },
                        new
                        {
                            Id = 33,
                            Name = "حي الخضراء"
                        },
                        new
                        {
                            Id = 34,
                            Name = "حي الشهداء"
                        },
                        new
                        {
                            Id = 35,
                            Name = "حي الإفراز"
                        },
                        new
                        {
                            Id = 36,
                            Name = "حي المثنى"
                        },
                        new
                        {
                            Id = 37,
                            Name = "حي صلاح الدين"
                        },
                        new
                        {
                            Id = 38,
                            Name = "حي الشرطة"
                        },
                        new
                        {
                            Id = 39,
                            Name = "حي القلعة"
                        },
                        new
                        {
                            Id = 40,
                            Name = "حي الكهرومائية"
                        },
                        new
                        {
                            Id = 41,
                            Name = "حي دور السكك"
                        },
                        new
                        {
                            Id = 42,
                            Name = "حي الزهور"
                        },
                        new
                        {
                            Id = 43,
                            Name = "حي الصعيوية"
                        },
                        new
                        {
                            Id = 44,
                            Name = "حي الشيخ رياح"
                        },
                        new
                        {
                            Id = 45,
                            Name = "حي مكيشيفة"
                        });
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("is_admin");

                    b.Property<string>("Password")
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("password");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasDatabaseName("ix_users_user_name");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAdmin = true,
                            Password = "admin",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.Branch", b =>
                {
                    b.HasOne("AinAlAtaaFoundation.Models.Clan", "Clan")
                        .WithMany("Branches")
                        .HasForeignKey("ClanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_branches_clans_clan_id");

                    b.Navigation("Clan");
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.BranchRepresentative", b =>
                {
                    b.HasOne("AinAlAtaaFoundation.Models.Branch", "Branch")
                        .WithMany("BranchRepresentatives")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_branch_representative_branches_branch_id");

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.User", b =>
                {
                    b.OwnsOne("AinAlAtaaFoundation.Models.DeletableModel", "DeletableModel", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("INTEGER")
                                .HasColumnName("id");

                            b1.Property<DateTime?>("DeletedAt")
                                .HasColumnType("TEXT")
                                .HasColumnName("deletable_model_deleted_at");

                            b1.Property<bool>("IsDeleted")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER")
                                .HasDefaultValue(false)
                                .HasColumnName("deletable_model_is_deleted");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_users_id");
                        });

                    b.Navigation("DeletableModel");
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.Branch", b =>
                {
                    b.Navigation("BranchRepresentatives");
                });

            modelBuilder.Entity("AinAlAtaaFoundation.Models.Clan", b =>
                {
                    b.Navigation("Branches");
                });
#pragma warning restore 612, 618
        }
    }
}

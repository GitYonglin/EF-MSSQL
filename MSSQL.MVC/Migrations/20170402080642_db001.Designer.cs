using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MSSQL.MVC.Models;

namespace MSSQL.MVC.Migrations
{
    [DbContext(typeof(Model))]
    [Migration("20170402080642_db001")]
    partial class db001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MSSQL.MVC.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("MSSQL.MVC.Models.BlogCategory", b =>
                {
                    b.Property<int>("BlogId");

                    b.Property<int>("CategoryId");

                    b.HasKey("BlogId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BlogCategory");
                });

            modelBuilder.Entity("MSSQL.MVC.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categorys");
                });

            modelBuilder.Entity("MSSQL.MVC.Models.Img", b =>
                {
                    b.Property<int>("ImgID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogId");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("ImgID");

                    b.HasIndex("BlogId");

                    b.ToTable("Imgs");
                });

            modelBuilder.Entity("MSSQL.MVC.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("MSSQL.MVC.Models.BlogCategory", b =>
                {
                    b.HasOne("MSSQL.MVC.Models.Blog", "Blog")
                        .WithMany("BlogCategorys")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MSSQL.MVC.Models.Category", "Category")
                        .WithMany("BlogCategorys")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSSQL.MVC.Models.Img", b =>
                {
                    b.HasOne("MSSQL.MVC.Models.Blog", "Blog")
                        .WithMany("Imgs")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSSQL.MVC.Models.Post", b =>
                {
                    b.HasOne("MSSQL.MVC.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

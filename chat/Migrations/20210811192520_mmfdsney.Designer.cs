// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using chat.Entites.Context;

namespace chat.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20210811192520_mmfdsney")]
    partial class mmfdsney
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("chat.Entites.MessageContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsCustomer")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MessageTopicId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MessageTopicId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageContents");
                });

            modelBuilder.Entity("chat.Entites.MessageTopic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("RiderMail")
                        .HasColumnType("text");

                    b.Property<string>("RiderName")
                        .HasColumnType("text");

                    b.Property<Guid>("WebSiteId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WebSiteId");

                    b.ToTable("MessageTopics");
                });

            modelBuilder.Entity("chat.Entites.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Mail")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("chat.Entites.WebSites", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("WebSiteName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WebSites");
                });

            modelBuilder.Entity("chat.Entites.MessageContent", b =>
                {
                    b.HasOne("chat.Entites.MessageTopic", "MessageTopic")
                        .WithMany("messageContents")
                        .HasForeignKey("MessageTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("chat.Entites.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MessageTopic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("chat.Entites.MessageTopic", b =>
                {
                    b.HasOne("chat.Entites.WebSites", "WebSite")
                        .WithMany("MessageTopics")
                        .HasForeignKey("WebSiteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WebSite");
                });

            modelBuilder.Entity("chat.Entites.User", b =>
                {
                    b.HasOne("chat.Entites.User", null)
                        .WithMany("Users")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("chat.Entites.MessageTopic", b =>
                {
                    b.Navigation("messageContents");
                });

            modelBuilder.Entity("chat.Entites.User", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("chat.Entites.WebSites", b =>
                {
                    b.Navigation("MessageTopics");
                });
#pragma warning restore 612, 618
        }
    }
}

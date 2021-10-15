﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebApi.Entity
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DatabaseGeneratedOption.Computed ile primary key belli bir işlemlerden geçirilerek belirlenebilir.
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebApi.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // DatabaseGeneratedOption.Computed ile primary key belli bir işlemlerden geçirilerek belirlenebilir.
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int PageCount { get; set; }
        
        public int AuthorId { get; set; }

        
        public DateTime PublishDate { get; set; }
    }
}
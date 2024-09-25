using System;
using AutoMapper;
using BookCatalog.Entities;
using BookCatalog.Models.Dtos;

namespace BookCatalog.Models.Mapping;

public class BookProfile : Profile
{
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
}

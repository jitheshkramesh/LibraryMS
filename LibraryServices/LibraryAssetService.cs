using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;

        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
        }
        public LibraryAsset GetById(int Id)
        {
            return GetAll()
                 .FirstOrDefault(asset => asset.Id == Id);
        }

        public LibraryBranch GetCurrentLocation(int Id)
        {
            return GetById(Id).Location;
        }                 

        public string GetDeweyIndex(int Id)
        {
            if (_context.Books.Any(book => book.Id == Id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id==Id).DeweyIndex;
            }
            else {
                return "";
            }
        }

        public string GetIsbn(int Id)
        {
            if (_context.Books.Any(book => book.Id == Id))
            {
                return _context.Books
                    .FirstOrDefault(book => book.Id == Id).ISBN;
            }
            else
            {
                return "";
            }
        }

        public string GetTitle(int Id)
        {
            return _context.LibraryAssets
                     .FirstOrDefault(book => book.Id == Id).Title;
        }

        public string GetType(int Id)
        {
            var book = _context.LibraryAssets.OfType<Book>()
                 .Where(b => b.Id == Id);
            return book.Any() ? "Book" : "Video";
        }

        public string GetAuthororDirector(int Id)
        {
            var isBook = _context.LibraryAssets.OfType<Book>()
                 .Where(asset => asset.Id == Id).Any();

            var isVideo = _context.LibraryAssets.OfType<Video>()
                 .Where(asset => asset.Id == Id).Any();

            return isBook ?
                _context.Books.FirstOrDefault(book => book.Id == Id).Author :
                _context.Videos.FirstOrDefault(video => video.Id == Id).Director
                ?? "UnKnown";
        }
    }
}

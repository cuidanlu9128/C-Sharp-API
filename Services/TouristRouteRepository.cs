using FakeXiecheng.API.Databases;
using FakeXiecheng.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeXiecheng.API.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _context;

        public TouristRouteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TouristRoute> GetTouristRouteAsync(Guid touristRouteId)
        {
            return await _context.touristRoutes.Include(t => t.TouristRoutePictures).FirstOrDefaultAsync(n => n.Id == touristRouteId);
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesAsync(string keyword, string ratingOperator, int? ratingValue)
        {
            IQueryable<TouristRoute> result = _context.touristRoutes.Include(t => t.TouristRoutePictures);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                result = result.Where(t => t.Title.Contains(keyword));
            }
            if (ratingValue >= 0)
            {
                switch(ratingOperator)
                {
                    case "largerThan":
                        result = result.Where(t => t.Rating >= ratingValue);
                        break;
                    case "lessThan":
                        result = result.Where(t => t.Rating <= ratingValue);
                        break;
                    case "equalTo":
                        result = result.Where(t => t.Rating == ratingValue);
                        break;
                }
            }

            return await result.ToListAsync();
        }

        public async Task<bool> TouristRouteExistsAsync(Guid touristRouteId)
        {
            return await _context.touristRoutes.AnyAsync(t => t.Id == touristRouteId);
        }

        public async Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteIdAsync(Guid touristRouteId)
        {
            return await _context.touristRoutePictures.Where(p => p.TouristRouteId == touristRouteId).ToListAsync();
        }

        public async Task<TouristRoutePicture> GetPictureAsync(int pictureId)
        {
            return await _context.touristRoutePictures.Where(p => p.Id == pictureId).FirstOrDefaultAsync();
        }

        public void AddTouristRoute(TouristRoute touristRoute)
        {
            if (touristRoute == null)
            {
                throw new ArgumentException(nameof(touristRoute));
            }
            _context.touristRoutes.Add(touristRoute);
        }

        public void AddTouristRoutePicture(Guid touristRouteId, TouristRoutePicture touristRoutePicture)
        {
            if (touristRouteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(touristRouteId));
            }
            if (touristRoutePicture == null)
            {
                throw new ArgumentNullException(nameof(touristRoutePicture));
            }
            touristRoutePicture.TouristRouteId = touristRouteId;
            _context.touristRoutePictures.Add(touristRoutePicture);
        }
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteTouristRoute(TouristRoute touristRoute)
        {
            _context.touristRoutes.Remove(touristRoute);
        }
        public void DeleteTouristRoutePicture(TouristRoutePicture touristRoutePicture)
        {
            _context.touristRoutePictures.Remove(touristRoutePicture);
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesByIDList(IEnumerable<Guid> ids)
        {
            return await _context.touristRoutes.Where(t => ids.Contains(t.Id)).ToListAsync();
        }

        public void DeleteTouristRoutes(IEnumerable<TouristRoute> touristRoutes)
        {
            _context.touristRoutes.RemoveRange(touristRoutes);
        }
    }
}

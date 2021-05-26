using System.Linq;

namespace Business.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using AutoMapper;

    using Business.IRepository;
    using DataAccess;
    using DataAccess.Models;
    using Models;

    public class PotHandler : IHandle<PotDTO>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PotHandler(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<PotDTO> Create(PotDTO entity)
        {
            var pot = _mapper.Map<PotDTO, Pot>(entity);
            await _db.Pots.AddAsync(pot);
            await _db.SaveChangesAsync();

            return _mapper.Map<Pot, PotDTO>(pot);
        }

        public async Task<PotDTO> Update(PotDTO entity)
        {
            var pot = await _db.Pots.FindAsync(entity.Id);
            pot.Activated = entity.Activated ?? pot.Activated;
            pot.Name = entity.Name ?? pot.Name;
            pot.TotalAmount = entity.TotalAmount ?? pot.TotalAmount;

            _db.Pots.Update(pot);
            await _db.SaveChangesAsync();

            return _mapper.Map<Pot, PotDTO>(pot);
        }

        public async Task<int> Delete(Guid id)
        {
            try
            {
                _db.Pots.Remove(await _db.Pots.FindAsync(id));
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public async Task<PotDTO> Get(Guid id)
        {
            var pot = await _db.Pots.FindAsync(id);
            return _mapper.Map<Pot, PotDTO>(pot);
        }

        public async Task<ICollection<PotDTO>> GetAll()
        {
            var pots = await _db.Pots.ToListAsync();
            return _mapper.Map<List<Pot>, List<PotDTO>>(pots);
        }

        public async Task<ICollection<PotDTO>> GetAll(Guid userId)
        {
            var pots = await _db.Pots.Where(u=>u.UserId==userId).ToListAsync();
            return _mapper.Map<List<Pot>, List<PotDTO>>(pots);
        }
    }
}

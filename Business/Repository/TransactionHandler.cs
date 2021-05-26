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

    public class TransactionHandler : IHandle<TransactionDTO>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public TransactionHandler(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<TransactionDTO> Create(TransactionDTO entity)
        {
            var transaction = _mapper.Map<TransactionDTO, Transaction>(entity);
            await _db.Transactions.AddAsync(transaction);
            await _db.SaveChangesAsync();

            return _mapper.Map<Transaction, TransactionDTO>(transaction);
        }

        public async Task<TransactionDTO> Update(TransactionDTO entity)
        {
            var transaction = await _db.Transactions.FindAsync(entity.Id);
            transaction.Amount = entity.Amount ?? transaction.Amount;
            

            _db.Transactions.Update(transaction);
            await _db.SaveChangesAsync();

            return _mapper.Map<Transaction, TransactionDTO>(transaction);
        }

        public async Task<int> Delete(Guid id)
        {
            try
            {
                _db.Transactions.Remove(await _db.Transactions.FindAsync(id));
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public async Task<TransactionDTO> Get(Guid id)
        {
            var transaction = await _db.Transactions.FindAsync(id);
            return _mapper.Map<Transaction, TransactionDTO>(transaction);
        }

        public async Task<ICollection<TransactionDTO>> GetAll()
        {
            var transaction = await _db.Transactions.ToListAsync();
            return _mapper.Map<List<Transaction>, List<TransactionDTO>>(transaction);
        }

        public async Task<ICollection<TransactionDTO>> GetAll(Guid potId)
        {
            var transaction = await _db.Transactions
                .Where(t=>t.PotId==potId).ToListAsync();
            return _mapper.Map<List<Transaction>, List<TransactionDTO>>(transaction);
        }
    }
}
